namespace FluxoDeCaixa.Domain.Abstractions.Models
{
    public abstract class Entity
    {
        private Guid _id = Guid.NewGuid();
        private bool _isNew = true;

        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                _isNew = false;
            }
        }

        public bool IsNew { get => _isNew; }
    }
}