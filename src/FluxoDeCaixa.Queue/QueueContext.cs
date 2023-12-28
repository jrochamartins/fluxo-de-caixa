using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FluxoDeCaixa.Queue
{
    public class QueueContext : IDisposable
    {
        private readonly QueueContextOptions _options;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public QueueContext(IOptionsMonitor<QueueContextOptions> options)
        {
            _options = options.CurrentValue;
            var factory = new ConnectionFactory() { HostName = _options.ASPNET_RABBITMQ_CONNECTION_STRING };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _options.ASPNET_RABBITMQ_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(byte[] message, string exchange = "") =>
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: _options.ASPNET_RABBITMQ_QUEUE,
                basicProperties: null,
                body: message);

        public EventingBasicConsumer CreateConsumer() =>
           new(_channel);

        public void RegisterConsumer(IBasicConsumer consumer) =>
            _channel.BasicConsume(queue: _options.ASPNET_RABBITMQ_QUEUE, autoAck: true, consumer: consumer);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _channel?.Dispose();
                _connection?.Dispose();
            }
        }
    }
}
