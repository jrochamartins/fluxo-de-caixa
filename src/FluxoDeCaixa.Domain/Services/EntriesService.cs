using FluxoDeCaixa.Domain.Adapters;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.Contracts;
using FluxoDeCaixa.Domain.Validators;

namespace FluxoDeCaixa.Domain.Services
{
    public class EntriesService(INotifier notifier, IEntriesRepository repository, IQueuePublisher publisher) : BaseService(notifier), IEntriesService
    {
        public async Task CreateAsync(Entry entry)
        {
            if (!Validate(new EntryValidator(), entry))
                return;
            await repository.CreateAsync(entry);

            publisher.Publish(entry);
        }
    }
}
