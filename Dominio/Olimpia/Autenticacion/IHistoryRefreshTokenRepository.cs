namespace Dominio.Olimpia.Autenticacion
{
    public interface IHistoryRefreshTokenRepository
    {
        void Add(HistoryRefreshToken historyRefreshToken);
    }
}
