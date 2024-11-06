using System;
using System.Threading.Tasks;

namespace Athenas.RabbitMQ.Interfaces
{
    public interface IHandlerConsumerException<T> : IHandlerConsumer<T>
    {
        void NotDeserializedConsume(byte[] message, Exception ex);
    }
}