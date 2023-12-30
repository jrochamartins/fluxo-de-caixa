namespace FluxoDeCaixa.Domain.Abstractions.Adapters
{
    public interface IQueuePublisher
    {
        void Publish(object message);
    }
}
