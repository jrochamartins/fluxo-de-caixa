namespace FluxoDeCaixa.Domain.Adapters
{
    public interface IQueuePublisher
    {
        void Publish(object message);
    }
}
