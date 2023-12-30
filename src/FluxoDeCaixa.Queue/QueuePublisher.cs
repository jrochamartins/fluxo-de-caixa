using FluxoDeCaixa.Domain.Abstractions.Adapters;
using Newtonsoft.Json;
using System.Text;

namespace FluxoDeCaixa.Queue
{
    public class QueuePublisher(QueueContext context) : IQueuePublisher
    {
        public void Publish(object message) =>
            context.Publish(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }
}
