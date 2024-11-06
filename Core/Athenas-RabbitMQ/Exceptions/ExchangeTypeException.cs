using System;
using System.Collections.Generic;
using System.Text;

namespace Athenas.RabbitMQ.Exceptions
{
    [Serializable]
    public class ExchangeTypeException : Exception
    {
        public ExchangeTypeException(string message) : base(message) { }

    }
}
