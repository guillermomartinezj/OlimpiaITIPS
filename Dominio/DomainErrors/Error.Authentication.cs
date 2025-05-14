using ErrorOr;

namespace Dominio.DomainErrors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error DateOutOfRange => Error.Validation("Autenticación.Fechas", "La fecha de vencimiento no es válida");
        }
    }
}
