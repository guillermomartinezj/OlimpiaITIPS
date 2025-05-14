using System.Net;

namespace Infraestructura
{
    public class ServerInfoProvider
    {
        public string GetServerName()
        {
            return Dns.GetHostName();
        }

        public IPAddress[] GetServerIPAdresses()
        {
            return Dns.GetHostAddresses(GetServerName());
        }
    }
}
