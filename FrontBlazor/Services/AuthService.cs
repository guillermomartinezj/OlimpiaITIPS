using FrontBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace FrontBlazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, TokenService tokenService, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Login/Authenticate", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                    //if (result != null && result.Success && !string.IsNullOrEmpty(result.Token))
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        await _tokenService.SetToken(result.Token);
                        ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
                        return result;
                    }

                    return new AuthResponse { Success = false, Message = result?.Message ?? "Credenciales inválidas" };
                }

                // Manejar diferentes códigos de estado
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new AuthResponse { Success = false, Message = "Usuario o contraseña incorrectos" };
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return new AuthResponse { Success = false, Message = "Datos de inicio de sesión inválidos" };
                }

                return new AuthResponse { Success = false, Message = $"Error de servidor: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new AuthResponse { Success = false, Message = $"Error de conexión: {ex.Message}" };
            }
        }

        public async Task Logout()
        {
            await _tokenService.RemoveToken();
            ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _tokenService.GetToken();
            return !string.IsNullOrEmpty(token);
        }
    }
}
