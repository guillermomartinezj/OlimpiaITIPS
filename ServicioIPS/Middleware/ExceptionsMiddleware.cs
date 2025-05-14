using Aplicacion.Common.Exceptions;
using Azure.Core;
using System.Net;
using System.Text.Json;

namespace ServicioIPS.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context is null)
            {
                return;
            }

            try
            {
                await _next.Invoke(context);
            }
            catch (ExceptionAuthentication ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                await ProcessErrorAsync(context, (int)HttpStatusCode.Forbidden, ex.Message, "Excepción de Negocio");
            }
            catch (ExceptionBusiness ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                await ProcessErrorAsync(context, (int)HttpStatusCode.PreconditionFailed, ex.Message, "Excepción de Negocio");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                await ProcessErrorAsync(context, (int)HttpStatusCode.InternalServerError, ex.Message, "Excepción del servidor");
            }
        }

        public async Task ProcessErrorAsync(HttpContext context, int statusCode, string message, string title)
        {
            ErrorMiddleware errorMiddleware = new()
            {
                Type = "",
                Title = title,
                Status = statusCode,
                TraceId = Guid.NewGuid().ToString(),
                Errors = new ErrorType
                {
                    Message = new List<string> { message }
                }
            };

            var result = JsonSerializer.Serialize(errorMiddleware);

            context.Response.ContentType = ContentType.ApplicationJson.ToString();
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
