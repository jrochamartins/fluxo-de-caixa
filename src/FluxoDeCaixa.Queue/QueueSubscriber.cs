using FluxoDeCaixa.Domain.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Queue
{
    public class QueueSubscriber(
        QueueContext context,
        IServiceProvider serviceProvider) :
        IQueueSubscriber
    {
        private readonly QueueContext _context = context;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public void Register()
        {
            var consumer = _context.CreateConsumer();
            consumer.Received += (sender, args) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IQueueSubscriberHandler>();
                handler?.Handle(args.Body);
            };

            _context.RegisterConsumer(consumer);
        }

        public void Deregister()
        {
            _context.Dispose();
        }
    }
}
