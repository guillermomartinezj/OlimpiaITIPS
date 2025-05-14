using Aplicacion.Olimpia.Authentications.Command.Login;
using Aplicacion.Olimpia.Authentications.Command.RefreshToken;

namespace ServicioIPS.Controllers.V1.Olimpia
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiController
    {
        private readonly ISender _mediator;

        public LoginController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginAuthenticationCommand command)
        {
            var createResult = await _mediator.Send(command);

            return createResult.Match(
                loginResponse => Ok(loginResponse),
                errors => Problem(errors)
            );
        }

        [HttpPut]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
        {
            var createResult = await _mediator.Send(command);

            return createResult.Match(
                loginResponse => Ok(loginResponse),
                errors => Problem(errors)
            );
        }

    }
}
