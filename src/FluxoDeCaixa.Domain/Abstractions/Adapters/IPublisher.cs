namespace FluxoDeCaixa.Domain.Abstractions.Adapters
{
    public interface IPublisher
    {
        Task PublishAsync<T>(T message);
    }
}
