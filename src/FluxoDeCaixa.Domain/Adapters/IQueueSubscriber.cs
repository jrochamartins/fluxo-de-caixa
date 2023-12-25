namespace FluxoDeCaixa.Domain.Adapters
{
    public interface IQueueSubscriber
    {
        void Register();

        void Deregister();
    }
}
