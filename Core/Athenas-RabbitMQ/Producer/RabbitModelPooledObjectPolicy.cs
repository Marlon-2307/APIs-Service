
using System;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using Athenas.RabbitMQ.Exceptions;

namespace Publisher.Infraestructure.RabbitMQ
{
    public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly Uri _urlConnection;
        private IConnection _connection;
        private IModel _channel;

        public RabbitModelPooledObjectPolicy(Uri urlConnection)
        {
            _urlConnection = urlConnection;
            GetConnection();
        }

        private void GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory() { Uri = _urlConnection };
                _connection = factory.CreateConnection();
                CreateChannel();
            }
            catch (Exception ex)
            {
                throw new ConnectionRabbitException("No es posible conectarse al servidor de Rabbitmq, por favor valide la cadena de conexión", ex);
            }
        }

        private void CreateChannel()
        {
            try
            {
                _channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                throw new ConnectionRabbitException("No es posible establecer el canal, por favor validar los parámetros de canales", ex);
            }
        }

        public IModel Create()
        {
            if(!_connection.IsOpen)
                GetConnection();

            Return(_channel);

            return _channel;
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
            {
                return true;
            }
            else
            {
                obj?.Dispose();
                CreateChannel();
                return false;
            }
        }
    }
}