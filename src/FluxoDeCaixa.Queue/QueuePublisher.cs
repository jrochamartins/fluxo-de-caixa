using FluxoDeCaixa.Domain.Adapters;
using Newtonsoft.Json;
using System.Text;

namespace FluxoDeCaixa.Queue
{
    public class QueuePublisher(QueueContext context) : IQueuePublisher
    {
        private readonly QueueContext _context = context;

        public void Publish(object message) =>
            _context.Publish(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }
}
