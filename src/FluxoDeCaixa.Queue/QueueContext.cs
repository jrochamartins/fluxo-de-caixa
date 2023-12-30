using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FluxoDeCaixa.Queue
{
    public class QueueContext : IDisposable
    {
        private readonly QueueContextOptions _options;
        private readonly IConnection _connection;
        private readonly IModel _model;

        public QueueContext(IOptionsMonitor<QueueContextOptions> options)
        {
            _options = options.CurrentValue;
            var factory = new ConnectionFactory() { HostName = _options.RABBITMQ_CONNECTION_STRING };
            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(queue: _options.RABBITMQ_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(byte[] message, string exchange = "") =>
            _model.BasicPublish(exchange: exchange, routingKey: _options.RABBITMQ_QUEUE, basicProperties: null, body: message);

        public EventingBasicConsumer CreateConsumer() => new(_model);

        public void RegisterConsumer(IBasicConsumer consumer) =>
            _model.BasicConsume(queue: _options.RABBITMQ_QUEUE, autoAck: true, consumer: consumer);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _model?.Dispose();
                _connection?.Dispose();
            }
        }
    }
}
