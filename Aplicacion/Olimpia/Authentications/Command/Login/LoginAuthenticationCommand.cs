using Dominio.Olimpia.Autenticacion;
using ErrorOr;
using MediatR;

namespace Aplicacion.Olimpia.Authentications.Command.Login
{
    public record LoginAuthenticationCommand(string UserName, string Password)
     : IRequest<ErrorOr<LoginResponseDto>>;
}
