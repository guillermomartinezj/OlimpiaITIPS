namespace Aplicacion.Common.Exceptions
{
    public class ExceptionBusiness : Exception
    {
        public ExceptionBusiness()
        {
        }

        public ExceptionBusiness(string message) : base(message)
        {

        }

        public ExceptionBusiness(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
