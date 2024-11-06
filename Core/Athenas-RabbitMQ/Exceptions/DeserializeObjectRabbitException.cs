using System;
using System.Collections.Generic;
using System.Text;

namespace Athenas.RabbitMQ.Exceptions
{
    [Serializable]
    public class DeserializeObjectRabbitException : Exception
    {
        public DeserializeObjectRabbitException(string message, Exception inner) : base(message, inner) { }

    }
}
