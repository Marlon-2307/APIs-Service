namespace Athenas.RabbitMQ.Interfaces
{
    public interface IEventBus
    {
        void Publish<T>(T @event, string exchangeName, string exchangeType, string routeKey) where T : class;// Event;

    }
}