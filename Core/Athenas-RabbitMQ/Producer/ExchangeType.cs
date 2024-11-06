using System;
using System.Collections.Generic;
using System.Text;

namespace Athenas.RabbitMQ.Producer
{
    public static class ExchangeType
    {
        public static string Direct = "direct";
        public static string Fanout = "fanout";
        public static string Topic = "topic";

    }
}
