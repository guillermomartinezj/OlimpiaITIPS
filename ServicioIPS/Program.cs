using Microsoft.AspNetCore.Hosting;

namespace ServicioIPS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.Sources.Clear();
                    config.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (args != null) config.AddCommandLine(args);
                });
    }
}

