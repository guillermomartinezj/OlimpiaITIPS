using Dominio.Primitives;

namespace Dominio.Olimpia.Autenticacion
{
    public class HistoryRefreshToken : AggregateRoot
    {
        public long Id { get; set; }
        public long AuthenticationId { get; set; }
        public long ClientId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
