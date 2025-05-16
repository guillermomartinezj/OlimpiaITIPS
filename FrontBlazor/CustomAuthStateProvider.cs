using FrontBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontBlazor
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly TokenService _tokenService;
        private readonly static AuthenticationState _anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

        public CustomAuthStateProvider(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenService.GetToken();

            if (string.IsNullOrEmpty(token))
                return _anonymous;

            try
            {
                return CreateAuthenticationState(token);
            }
            catch
            {
                await _tokenService.RemoveToken();
                return _anonymous;
            }
        }

        private static AuthenticationState CreateAuthenticationState(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
