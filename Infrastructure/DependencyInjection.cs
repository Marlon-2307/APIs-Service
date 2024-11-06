using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Athenas.EjemploTemplateApi.Domain.Interfaces;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddLoggingWithSerilog(this IServiceCollection services)
    {

        services.AddApplicationInsightsTelemetry();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        // Register the Serilog logger with DI
        services.AddSingleton(Log.Logger);

        return services;
    }
}
