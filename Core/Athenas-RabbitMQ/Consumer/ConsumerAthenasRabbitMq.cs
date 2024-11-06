using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Athenas.RabbitMQ.EventHandlers;
using Athenas.RabbitMQ.Interfaces;
using Athenas.RabbitMQ.Settings;

namespace Athenas.RabbitMQ
{
    public class ConsumerAthenasRabbitMq<T>
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly ServiceProvider _serviceProvider;
        private readonly ILogger<IHandlerConsumer<T>> _logger;
        private ConsumeQueueHosted<T> _consumeQueue;
        private readonly int _channelNumbers;

        public ConsumerAthenasRabbitMq(Uri urlConnection, string queueName, IServiceCollection services, int channelNumbers = 1)
        {
            _channelNumbers = channelNumbers;
            _serviceProvider = services.BuildServiceProvider();
            _rabbitMqSettings = new RabbitMqSettings
            {
                UrlConnection = urlConnection ?? throw new ArgumentNullException(nameof(urlConnection)),
                QueueName = queueName ?? throw new ArgumentNullException(nameof(queueName))
            };
            _logger = _serviceProvider.GetService<ILoggerFactory>().CreateLogger<IHandlerConsumer<T>>();
        }

        public ConsumerAthenasRabbitMq<T> AutoAckCheck(bool autoack)
        {
            _rabbitMqSettings.AutoAck = autoack;
            return this;
        }

        public ConsumeQueueHosted<T> Build()
        {
            _consumeQueue = new ConsumeQueueHosted<T>(_rabbitMqSettings, _serviceProvider, _logger, _channelNumbers);
            return _consumeQueue;
        }

        public void ResetConnection()
        {
            _consumeQueue?.Dispose();
            Build();
        }
    }
}