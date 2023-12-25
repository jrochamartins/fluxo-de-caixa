using FluxoDeCaixa.Domain.Adapters;
using FluxoDeCaixa.Domain.Models;
using Newtonsoft.Json;
using System.Text;

namespace FluxoDeCaixa.Queue.Handlers
{
    public abstract class BaseQueueSubscriberHandler<TEntity> : IQueueSubscriberHandler where TEntity : Entity
    {
        public abstract void Execute(TEntity payload);

        public void Handle(ReadOnlyMemory<byte> message)
        {
            var body = Encoding.UTF8.GetString(message.ToArray());
            var payload = JsonConvert.DeserializeObject<TEntity>(body);
            if (payload != null)
                Execute(payload);
        }
    }
}
