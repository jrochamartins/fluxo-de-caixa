namespace FluxoDeCaixa.Domain.Adapters
{
    public interface IQueueSubscriberHandler
    {
        void Handle(ReadOnlyMemory<byte> message);
    }
}
