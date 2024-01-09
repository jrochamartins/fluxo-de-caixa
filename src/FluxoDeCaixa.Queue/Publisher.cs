using FluxoDeCaixa.Domain.Abstractions.Adapters;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.Queue
{
    public class Publisher(IPublishEndpoint bus, ILogger<Publisher> logger) : IPublisher
    {
        public Task PublishAsync<T>(T message)
        {
            logger.LogInformation("Publisher.PublishAsync started");
            return bus.Publish(message!);
        }
    }
}
