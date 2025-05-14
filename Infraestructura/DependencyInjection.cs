using Dominio.Olimpia.Autenticacion;
using Dominio.Primitives;
using Infraestructura.Persistence;
using Infraestructura.Persistence.Repositories;
using Infraestructura.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SqlAutentenicacion")));

            services.AddScoped<IUnitOfWorkAuthentication>(sp =>
                    sp.GetRequiredService<AuthenticationDbContext>());


            //Repository	
            services.AddScoped<IHistoryRefreshTokenRepository, HistoryRefreshTokenRepository>();
            services.AddScoped<ILoginIPSRepository, LoginIPSRepository>();


            //Service
            services.AddSingleton<ITokenService, TokenService>();

            return services;
        }
    }
}
