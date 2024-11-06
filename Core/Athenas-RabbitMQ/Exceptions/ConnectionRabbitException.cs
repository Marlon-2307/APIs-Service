using System;
using System.Collections.Generic;
using System.Text;

namespace Athenas.RabbitMQ.Exceptions
{
    [Serializable]
    public class ConnectionRabbitException : Exception
    {
        public ConnectionRabbitException(string message, Exception inner) : base(message, inner) { }

    }
}
