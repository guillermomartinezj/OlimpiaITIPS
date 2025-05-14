using Dominio.Olimpia.Autenticacion;
using ErrorOr;
using MediatR;

namespace Aplicacion.Olimpia.Authentications.Command.RefreshToken
{
    public record RefreshTokenCommand(string Token)
         : IRequest<ErrorOr<LoginResponseDto>>;
}
