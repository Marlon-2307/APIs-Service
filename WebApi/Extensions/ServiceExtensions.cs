using Azure.Identity;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.OpenApi.Models;
using Athenas.EjemploTemplateApi.Domain.Interfaces;
using Athenas.EjemploTemplateApi.WebApi.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace Athenas.EjemploTemplateApi.WebApi.Extensions
{

    /// <summary>
    /// Servicio para agregar inyeccion de dependencias de la capa de infraestructura
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        private static string AdjustSecretValues(IConfiguration configuration, string value)
        {
            return !string.IsNullOrEmpty(configuration[value]) ?
                configuration[value] :
                value;
        }
        private static Dictionary<string, string> AdjustHeaderSecrets(IConfiguration configuration, Dictionary<string, string> checkBalanceSetting)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            foreach (var item in checkBalanceSetting)
            {
                if (!string.IsNullOrEmpty(configuration[item.Value]))
                {
                    headers.Add(item.Key, configuration[item.Value]);
                }
                else
                {
                    headers.Add(item.Key, item.Value);
                }
            }

            return headers;
        }


        #region Swagger
        /// <summary>
        /// Configuración Swagger
        /// </summary>
        /// <param name="services">Colleccion de servicios</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc(SwaggerConfiguration.DocInfoVersion, new OpenApiInfo
                {
                    Title = SwaggerConfiguration.EndpointDescription,
                    Version = SwaggerConfiguration.DocInfoVersion
                });
            });
        }


        /// <summary>
        ///  swagger Middleware
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndPointEnviromentUrl, SwaggerConfiguration.EndpointDescription);
            });
        }
        /// <summary>
        /// Middleware for add Check to external components
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>

        #endregion
    }
}
