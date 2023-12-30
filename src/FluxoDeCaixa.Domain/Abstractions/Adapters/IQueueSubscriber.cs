namespace FluxoDeCaixa.Domain.Abstractions.Adapters
{
    public interface IQueueSubscriber
    {
        void Register();

        void Deregister();
    }
}
