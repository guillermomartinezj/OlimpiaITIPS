using Aplicacion.Common.Exceptions;
using Dominio.Olimpia.Autenticacion;
using Dominio.Primitives;

namespace Aplicacion.Olimpia.Authentications.Command.Login
{
    public sealed class LoginAuthenticationCommandHandler : IRequestHandler<LoginAuthenticationCommand, ErrorOr<LoginResponseDto>>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWorkAuthentication _unitOfWork;
        private readonly IHistoryRefreshTokenRepository _historyRefreshTokenRepository;
        private readonly ILoginIPSRepository _usersAgreementRepository;

        public LoginAuthenticationCommandHandler(
            IHistoryRefreshTokenRepository historyRefreshTokenRepository,
            ILoginIPSRepository usersAgreementRepository,
            IUnitOfWorkAuthentication unitOfWork,
            ITokenService tokenService)
        {
            _historyRefreshTokenRepository = historyRefreshTokenRepository ?? throw new ArgumentNullException(nameof(historyRefreshTokenRepository));
            _usersAgreementRepository = usersAgreementRepository ?? throw new ArgumentNullException(nameof(usersAgreementRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService)); ;
        }

        public async Task<ErrorOr<LoginResponseDto>> Handle(LoginAuthenticationCommand command, CancellationToken cancellationToken)
        {
            LoginResponseDto response = new();

            var (clientId, userName) = await _usersAgreementRepository
                .CheckPasswordSignInAsync(command.UserName, command.Password);

            if (clientId is not null && !string.IsNullOrEmpty(userName))
            {
                var (token, expirationDate) = _tokenService.GenerateToken(clientId.Value.ToString(), userName);
                response.Token = token;
                response.RefreshToken = _tokenService.GenerateRefreshToken();
                _historyRefreshTokenRepository.Add(new HistoryRefreshToken
                {
                    AuthenticationId = 1,
                    ClientId = (long)clientId,
                    CreationDate = DateTime.UtcNow,
                    ExpirationDate = expirationDate,
                    Token = token,
                    RefreshToken = response.RefreshToken
                });

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ExceptionAuthentication($"Se ha presentado un error al validar las credenciales");
            }

            return response;
        }
    }
}
