namespace FluxoDeCaixa.Domain.Abstractions.Models
{
    public abstract class Entity
    {
        public Guid Id = Guid.NewGuid();
    }
}