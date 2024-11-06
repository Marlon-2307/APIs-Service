using System;
using System.Collections.Generic;
using System.Text;

namespace Athenas.RabbitMQ.Settings
{
    public static class ExchangeType
    {
        public static string Fanout { get { return "fanout"; } }
        public static string Direct { get { return "direct"; } }
        public static string Topic { get { return "topic"; } }
    }
}
