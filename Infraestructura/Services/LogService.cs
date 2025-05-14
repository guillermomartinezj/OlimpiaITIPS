using Dominio.Olimpia.Autenticacion;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Services
{
    public class LogService : ITelemetryInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public LogService(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var tokenInfo = _tokenService.GetTokenInfo();

            if (telemetry is ISupportProperties supportProperties)
            {
                supportProperties.Properties["Usuario"] = tokenInfo?.UserName;
                supportProperties.Properties["Cliente"] = tokenInfo?.ClientId;
                supportProperties.Properties["Servidor"] = _configuration["Servidor"];
            }
        }

    }
}
