using FluentValidation;

namespace Aplicacion.Olimpia.Authentications.Command.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(r => r.Token)
                .NotEmpty();
        }
    }
}
