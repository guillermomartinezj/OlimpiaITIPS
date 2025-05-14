using FluentValidation;

namespace Aplicacion.Olimpia.Authentications.Command.Login
{
    public class LoginAuthenticationCommandValidator : AbstractValidator<LoginAuthenticationCommand>
    {
        public LoginAuthenticationCommandValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty();
            RuleFor(r => r.Password)
                .NotEmpty();
        }
    }
}
