using Athenas.EjemploTemplateApi.Application.Contracts;
using Athenas.EjemploTemplateApi.Application.UseCases;
using Athenas.EjemploTemplateApi.Domain.Interfaces;
using Athenas.EjemploTemplateApi.Infrastructure;
using Athenas.EjemploTemplateApi.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Athenas.EjemploTemplateApi.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddSqlDataBase(this IServiceCollection services, string stringConnection)
    {
        // Register use cases
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(stringConnection));

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Register use cases
        services.AddTransient<IPostsUseCase, PostsUseCase>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Register use cases
        services.AddTransient<IRepositoryPost, RepositoryPost>();

        return services;
    }

    public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
