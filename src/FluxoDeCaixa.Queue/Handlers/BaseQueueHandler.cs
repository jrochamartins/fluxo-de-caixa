using FluxoDeCaixa.Domain.Abstractions.Adapters;
using FluxoDeCaixa.Domain.Abstractions.Models;
using Newtonsoft.Json;
using System.Text;

namespace FluxoDeCaixa.Queue.Handlers
{
    public abstract class BaseQueueHandler<TEntity> where TEntity : Entity
    {
        public void Handle(ReadOnlyMemory<byte> message)
        {
            var body = Encoding.UTF8.GetString(message.ToArray());
            var payload = JsonConvert.DeserializeObject<TEntity>(body);
            if (payload != null)
                Handle(payload);
        }
        public abstract void Handle(TEntity payload);
    }
}
