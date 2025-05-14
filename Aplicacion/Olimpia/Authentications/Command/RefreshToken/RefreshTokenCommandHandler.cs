using Dominio.Olimpia.Autenticacion;
using Dominio.Primitives;

namespace Aplicacion.Olimpia.Authentications.Command.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<LoginResponseDto>>
    {
        private readonly IHistoryRefreshTokenRepository _historyRefreshTokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWorkAuthentication _unitOfWork;

        public RefreshTokenCommandHandler(
            IHistoryRefreshTokenRepository historyRefreshTokenRepository,
            IUnitOfWorkAuthentication unitOfWork,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService)); ;
            _historyRefreshTokenRepository = historyRefreshTokenRepository ?? throw new ArgumentNullException(nameof(historyRefreshTokenRepository)); ;
        }

        public async Task<ErrorOr<LoginResponseDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var (loginResponse, expirationDate) = _tokenService.RefrescarToken(command.Token);

            _historyRefreshTokenRepository.Add(new HistoryRefreshToken
            {
                AuthenticationId = 1,
                ClientId = 1,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = expirationDate,
                Token = loginResponse.Token,
                RefreshToken = loginResponse.RefreshToken
            });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return loginResponse;
        }
    }
}
