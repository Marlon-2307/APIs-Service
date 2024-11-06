using System;
using Microsoft.Extensions.DependencyInjection;
using Athenas.RabbitMQ.Interfaces;

namespace Athenas.RabbitMQ.Consumer
{
    public static class ConsumerRabbitServiceCollectionExtensions
    {
        /// <summary>
        /// Throw exception when error is not caught "Requeue Message", 
        ///     otherwise use IHandlerConsumerException<T>.NotDeserializedConsume to catch message when object cannot deserialize "Not Requeue Message".
        /// </summary>
        /// <param name="services"></param>
        /// <param name="urlConnection"></param>
        /// <param name="queueName"></param>
        /// <param name="Ack"></param>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddAthenasRabbitMqConsumer<TMessage, TImplementation>(this IServiceCollection services, Uri urlConnection, string queueName, int channelNumbers = 1, bool autoAck = false)
            where TMessage : class
            where TImplementation : class, IHandlerConsumer<TMessage>
        {
            services.AddTransient<IHandlerConsumer<TMessage>, TImplementation>();
            CreateCustomerAthenasRabbitMq<TMessage>(services, urlConnection, queueName, channelNumbers, autoAck);

            return services;
        }

        public static IServiceCollection AddAthenasRabbitMqConsumerWithException<TMessage, TImplementation>(this IServiceCollection services, Uri urlConnection, string queueName, int channelNumbers = 1, bool autoAck = false)
            where TMessage : class
            where TImplementation : class, IHandlerConsumerException<TMessage>
        {
            services.AddTransient<IHandlerConsumerException<TMessage>, TImplementation>();
            CreateCustomerAthenasRabbitMq<TMessage>(services, urlConnection, queueName, channelNumbers, autoAck);

            return services;
        }

        private static void CreateCustomerAthenasRabbitMq<TMessage>(IServiceCollection services, Uri urlConnection, string queueName, int channelNumbers, bool autoAck) where TMessage : class
        {
            services.AddHostedService(_ =>
            {
                return new ConsumerAthenasRabbitMq<TMessage>(
                    urlConnection, queueName, services, channelNumbers)
                    .AutoAckCheck(autoAck)
                    .Build();
            });
        }
    }
}