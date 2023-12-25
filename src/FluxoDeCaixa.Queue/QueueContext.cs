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

        public QueueContext(IOptionsMonitor<QueueContextOptions> optionsAccessor)
        {
            _options = optionsAccessor.CurrentValue;
            var factory = new ConnectionFactory() { HostName = _options.QueueHostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _options.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(byte[] message, string exchange = "") =>
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: _options.QueueName,
                basicProperties: null,
                body: message);

        public EventingBasicConsumer CreateConsumer() =>
           new(_channel);

        public void RegisterConsumer(IBasicConsumer consumer) =>
            _channel.BasicConsume(queue: _options.QueueName, autoAck: true, consumer: consumer);

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
