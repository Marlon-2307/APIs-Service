using System;

namespace Athenas.RabbitMQ.Settings
{
    public class RabbitMqSettings
    {
        public Uri UrlConnection { get; set; }
        public string QueueName { get; set; }
        public bool AutoAck { get; set; }

        public RabbitMqSettings()
        {
            AutoAck = false;
        }
    }

}