using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Domain.Exceptions
{
    [Serializable]
    public class DataBaseException : Exception
    {
        public DataBaseException(string message) : base(message)
        {
        }

        public DataBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataBaseException( 
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
