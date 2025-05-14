using Dominio.Primitives;

namespace Dominio.Olimpia.Autenticacion
{
    public class LoginIPS : AggregateRoot
    {
        public long IdLoginIPS { get; set; }
        public string? UserName { get; set; }
        public string? Clave { get; set; }
        public byte? IdTipoAccesoIPS { get; set; }

        public bool Estado { get; set; }

    }
}
