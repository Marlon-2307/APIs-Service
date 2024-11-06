using Publisher.Infraestructure.RabbitMQ;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using Athenas.RabbitMQ.Interfaces;
using System;

namespace Athenas.RabbitMQ
{
    public static class RabbitServiceCollectionExtensions
    {
        /// <summary>
        /// Throw Exception ConnectionRabbitException When Rabbit Conection, Create Channel Fail Or Parameters are invalid
        /// </summary>
        /// <param name="services"></param>
        /// <param name="urlConnection"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitConnection(this IServiceCollection services, Uri urlConnection)
        {          
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>(x=> new RabbitModelPooledObjectPolicy(urlConnection));
            services.AddSingleton<IEventBus, ProducerManager>();

            return services;
        }
    }
}