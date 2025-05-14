using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Olimpia.Autenticacion
{
    public interface ITokenService
    {
        (string, DateTime) GenerateToken(string idCliente, string usuario);
        string GenerateRefreshToken();
        (LoginResponseDto, DateTime) RefrescarToken(string token);
        TokenInfo? GetTokenInfo();
        int GetClientId();
    }
}
