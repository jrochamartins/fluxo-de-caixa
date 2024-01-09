using FluxoDeCaixa.Domain.Abstractions.Adapters;
using MassTransit;

namespace FluxoDeCaixa.Queue
{
    public class Publisher(IPublishEndpoint bus) : IPublisher
    {
        public async Task PublishAsync<T>(T message)
        {
            if (message != null) await bus.Publish(message);
        }
    }
}
