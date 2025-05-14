using Aplicacion.Common.Exceptions;
using Dominio.Olimpia.Autenticacion;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructura.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string KEY_USERNAME = "Usuario";
        private const string KEY_CLIENT_ID = "IdCliente";

        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public (string, DateTime) GenerateToken(string idCliente, string usuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key ?? string.Empty);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(type: KEY_CLIENT_ID, value: idCliente));
            claims.AddClaim(new Claim(type: KEY_USERNAME, value: usuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var expires = DateTime.UtcNow.AddMinutes(10);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expires,
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return (tokenCreado, expires);
        }

        public string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        public (LoginResponseDto, DateTime) RefrescarToken(string token)
        {
            LoginResponseDto response = new();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpirado = tokenHandler.ReadJwtToken(token);

            if (tokenExpirado.ValidTo > DateTime.UtcNow)
                throw new ExceptionAuthentication($"Token no ha expirado");

            string idCliente = tokenExpirado.Claims.First(x =>
                x.Type == KEY_CLIENT_ID).Value.ToString();

            string userName = tokenExpirado.Claims.First(x =>
            x.Type == KEY_USERNAME).Value.ToString();

            var (currentToken, expirationDate) = GenerateToken(idCliente, userName);
            response.Token = currentToken;
            response.RefreshToken = GenerateRefreshToken();


            return (response, expirationDate);
        }

        public TokenInfo? GetTokenInfo()
        {
            var user = _httpContextAccessor?.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated ?? false)
            {
                var userName = user.Claims.First(x => x.Type == KEY_USERNAME).Value.ToString();
                var clientId = user.Claims.First(x => x.Type == KEY_CLIENT_ID).Value.ToString();

                return new TokenInfo
                {
                    UserName = userName,
                    ClientId = clientId
                };
            }

            return null;
        }

        public int GetClientId()
        {
            var user = GetTokenInfo();
            if (!int.TryParse(user?.ClientId, out int clientId))
            {
                return 0;
            }
            return clientId;
        }
    }
}
