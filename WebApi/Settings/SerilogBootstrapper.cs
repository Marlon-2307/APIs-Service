using Infrastructure;
using Serilog;

namespace Athenas.EjemploTemplateApi.WebApi.Settings
{
    public static class SerilogBootstrapper
    {
        public static IHostBuilder UseSerilogWithCustomConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog();

            hostBuilder.ConfigureServices(services =>
            {
                services.AddLoggingWithSerilog();
            });

            return hostBuilder;
        }
    }
}
