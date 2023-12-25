using FluxoDeCaixa.Domain.Adapters;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.Contracts;
using FluxoDeCaixa.Domain.Validators;

namespace FluxoDeCaixa.Domain.Services
{
    public class EntriesService(
        INotifier notifier,
        IEntriesRepository repository,
        IQueuePublisher publisher) :
        BaseService(notifier),
        IEntriesService
    {
        private readonly IEntriesRepository _repository = repository;
        private readonly IQueuePublisher _publisher = publisher;

        public async Task CreateAsync(Entry entry)
        {
            if (!Validate(new EntryValidator(), entry))
                return;
            await _repository.CreateAsync(entry);

            _publisher.Publish(entry);
        }
    }
}
