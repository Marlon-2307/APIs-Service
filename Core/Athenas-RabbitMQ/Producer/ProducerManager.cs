using System.Text;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using Athenas.RabbitMQ.Interfaces;
using System;
using Newtonsoft.Json;
using Athenas.RabbitMQ.Exceptions;

namespace Athenas.RabbitMQ
{
    public class ProducerManager : IEventBus
    {
        private readonly DefaultObjectPool<IModel> _objectPool;

        public ProducerManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }

        /// <summary>
        /// Exchange Type (direct, topic, fanout)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="exchangeName"></param>
        /// <param name="exchangeType"></param>
        /// <param name="routeKey"></param>
        public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class
        {
            validateExchange(exchangeType);

            if (message == null)
                return;

            var channel = _objectPool.Get();

            try
            {
                channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);
            }
            catch (Exception ex)
            {
                var msg = channel.IsClosed 
                    ? channel.CloseReason.ReplyText 
                    : "The Channel Is Open Conection Ok,  Validate Publish Message and Parameters RabbitMQ";
                throw new ConnectionRabbitException(msg, ex);
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }

        private void validateExchange(string exchangeType) {
            if (exchangeType != ExchangeType.Fanout && exchangeType != ExchangeType.Direct && exchangeType != ExchangeType.Topic)
                throw new ExchangeTypeException("El tipo de exchange no es permitido, debe escoger  fanout, direct o topic");
        }

    }
    
}