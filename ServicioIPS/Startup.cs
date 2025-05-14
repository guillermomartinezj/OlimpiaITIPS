using Aplicacion;
using Infraestructura;
using ServicioIPS.Middleware;
using System.Net;

namespace ServicioIPS
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            services.AddPresentation()
                            .AddInfrastructure(Configuration)
                            .AddApplication()
                            .AddAuthenticationJwt(Configuration)
                            .AddAplicationInsights(Configuration)
                            .AddLogger(Configuration);

            /* para agregar servicios de terceros */

            //services.AddHttpClient("SercurId", client =>
            //{
            //    client.BaseAddress = new Uri(Configuration["SecurId:BaseUrl"] ?? string.Empty);
            //});
                       
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionsMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
