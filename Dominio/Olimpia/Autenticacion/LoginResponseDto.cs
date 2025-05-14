using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Olimpia.Autenticacion
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;

    }
}
