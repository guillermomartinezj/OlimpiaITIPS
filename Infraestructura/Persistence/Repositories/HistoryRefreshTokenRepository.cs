using Dominio.Olimpia.Autenticacion;

namespace Infraestructura.Persistence.Repositories
{
    public class HistoryRefreshTokenRepository : IHistoryRefreshTokenRepository
    {
        private readonly AuthenticationDbContext _context;

        public HistoryRefreshTokenRepository(AuthenticationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(HistoryRefreshToken historyRefreshToken) =>
            _context.HistoryRefreshTokens.Add(historyRefreshToken);
    }
}
