using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Athenas.RabbitMQ.Exceptions;
using Athenas.RabbitMQ.Interfaces;
using Athenas.RabbitMQ.Settings;

namespace Athenas.RabbitMQ.EventHandlers
{
    public class ConsumeQueueHosted<T> : BackgroundService
    {
        private readonly ILogger<IHandlerConsumer<T>> _logger;
        private readonly RabbitMqSettings _options;
        private IConnection _connection;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _channelNumbers;

        public ConsumeQueueHosted(RabbitMqSettings rabbitMqSettings, IServiceProvider serviceProvider, ILogger<IHandlerConsumer<T>> logger, int channelNumbers)
        {
            _options = rabbitMqSettings ?? throw new ArgumentNullException(nameof(rabbitMqSettings));
            _serviceProvider = serviceProvider;
            _logger = logger;
            _channelNumbers = channelNumbers < 1 ? 1 : channelNumbers;
            InitRabbitMQ();
        }

        public void InitRabbitMQ()
        {
            try
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    var factory = new ConnectionFactory { Uri = _options.UrlConnection };
                    _connection = factory.CreateConnection();
                }
            }
            catch (RabbitMQClientException ex)
            {
                throw new ConnectionRabbitException("No es posible conectarse a CloudAmqp, por favor valide la cadena de conexi�n", ex);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {                
                if (string.IsNullOrEmpty(_options.QueueName))
                    throw new ArgumentNullException("QueueName", "No est� configurado el nombre de la Cola (Queue) a la que desea suscribirse");

                for (var i = 0; i < _channelNumbers; i++)
                {
                    var channel = _connection.CreateModel();
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(_options.QueueName, _options.AutoAck, consumer);
                }
            }
           
            catch (OperationInterruptedException ex ) when (ex.Message.Contains("NOT_FOUND - no queue"))
            {
                throw new QueueRabbitNotExistException($"La cola (Queue) {_options.QueueName} no existe o no esta creada.", ex);
            }

            return Task.CompletedTask;
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs deliveryEventArgs)
        {
            IHandlerConsumer<T> callBack = _serviceProvider.GetService<IHandlerConsumer<T>>() 
                ?? _serviceProvider.GetService<IHandlerConsumerException<T>>();
                
            var eventingBasicConsumer = (EventingBasicConsumer) sender;
            
            try
            {
                var content = System.Text.Encoding.UTF8.GetString(deliveryEventArgs.Body.ToArray());
                T message = JsonConvert.DeserializeObject<T>(content);

                callBack.Consume(message)
                    .ConfigureAwait(false)
                    .GetAwaiter();
                eventingBasicConsumer.Model.BasicAck(deliveryEventArgs.DeliveryTag, false);
            }
            catch (JsonException ex)
            {
                _logger.LogError("Error Trying Deserialize RabbitMQ Message", ex);
                if(callBack is IHandlerConsumerException<T> iHandlerConsumerException)
                    iHandlerConsumerException.NotDeserializedConsume(deliveryEventArgs.Body.ToArray(), ex);
                eventingBasicConsumer.Model.BasicAck(deliveryEventArgs.DeliveryTag, false);
            }
            catch (RabbitMQClientException ex)
            {
                _logger.LogError("Error RabbitMQ Client, If Error Persist Please Reset Connection", ex);
                eventingBasicConsumer.Model.BasicNack(deliveryEventArgs.DeliveryTag, false, true);
            }
            catch (Exception ex)
            {
                var msg = $"Error Processing in Consume Method, {nameof(callBack)} Interface";
                _logger.LogError(msg, ex);
                eventingBasicConsumer.Model.BasicNack(deliveryEventArgs.DeliveryTag, false, true);
                eventingBasicConsumer.Model.Close(541, msg);
                throw;
            }
        }

        public override void Dispose()
        {
            _connection.Close();
            base.Dispose();
        }
    }
}