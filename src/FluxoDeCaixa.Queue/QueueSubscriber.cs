using FluxoDeCaixa.Domain.Abstractions.Adapters;
using FluxoDeCaixa.Queue.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Queue
{
    public class QueueSubscriber(QueueContext context, IServiceProvider serviceProvider) : IQueueSubscriber
    {
        public void Register()
        {
            var consumer = context.CreateConsumer();
            consumer.Received += (sender, args) =>
            {
                using var scope = serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<EntryQueueHandler>();
                handler?.Handle(args.Body);
            };
            context.RegisterConsumer(consumer);
        }

        public void Deregister() => context.Dispose();
    }
}
