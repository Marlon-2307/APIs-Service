using System;
using System.Collections.Generic;
using System.Text;

namespace Athenas.RabbitMQ.Exceptions
{
    [Serializable]
    public class QueueRabbitNotExistException : Exception
    {
        public QueueRabbitNotExistException(string message, Exception inner) : base(message, inner) { }

    }
}
