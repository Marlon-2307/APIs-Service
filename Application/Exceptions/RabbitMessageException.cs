using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.Exceptions
{
    [Serializable]
    public class RabbitMessageException : Exception
    {
        public RabbitMessageException() { }
        public RabbitMessageException(string message) : base(message) { }
        public RabbitMessageException(string message, Exception inner) : base(message, inner) { }
        protected RabbitMessageException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
