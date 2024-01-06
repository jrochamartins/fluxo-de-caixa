using FluxoDeCaixa.Domain.Abstractions.Adapters;
using MassTransit;

namespace FluxoDeCaixa.Queue
{
    public class Publisher(IPublishEndpoint bus) : IPublisher
    {
        public async Task PublishAsync(object message) => await bus.Publish(message);
    }
}
