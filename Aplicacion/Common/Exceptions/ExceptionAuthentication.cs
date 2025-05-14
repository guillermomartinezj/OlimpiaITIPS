namespace Aplicacion.Common.Exceptions
{
    public class ExceptionAuthentication : Exception
    {
        public ExceptionAuthentication()
        {
        }

        public ExceptionAuthentication(string message) : base(message)
        {

        }

        public ExceptionAuthentication(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
