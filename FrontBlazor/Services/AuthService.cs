using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FrontBlazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;
        private readonly CustomAuthStateProvider _authProvider;

        public AuthService(HttpClient httpClient, ISessionStorageService sessionStorage, AuthenticationStateProvider authProvider)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
            _authProvider = (CustomAuthStateProvider)authProvider;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Login/Authenticate", new { Username = username, Password = password });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            if (result == null || string.IsNullOrWhiteSpace(result.Token))
                return false;

            // Guardar el token
            await _sessionStorage.SetItemAsync("authToken", result.Token);

            // Notificar cambio de estado
            _authProvider.MarkUserAsAuthenticated(result.Token);

            // Configurar HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            return true;
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.RemoveItemAsync("authToken");
            _authProvider.MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _sessionStorage.GetItemAsync<string>("authToken");
        }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
    }
}
