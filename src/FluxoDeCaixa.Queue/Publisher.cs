using FluxoDeCaixa.Domain.Abstractions.Adapters;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.Queue
{
    public class Publisher(IPublishEndpoint bus, ILogger<Publisher> logger) : IPublisher
    {
        public Task PublishAsync<T>(T message)
        {
            logger.LogInformation("{Object}.{Method} started", nameof(Publisher), nameof(PublishAsync));

            return bus.Publish(message!);
        }
    }
}
