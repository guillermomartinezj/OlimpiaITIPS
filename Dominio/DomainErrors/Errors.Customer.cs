using ErrorOr;

namespace Dominio.DomainErrors
{
    public static partial class Errors
    {
        public static class Customer
        {
            public static Error PhoneNumberWithBadFormat =>
                Error.Validation("Número.de.teléfonodelcliente", "El número de teléfono no tiene formato válido.");

            public static Error AddressWithBadFormat =>
                Error.Validation("Dirección del cliente", "La dirección no es válida.");
        }
    }
}
