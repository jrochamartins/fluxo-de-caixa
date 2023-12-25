namespace FluxoDeCaixa.Domain.Models
{
    public abstract class Entity
    {
        private bool isNew = true;
        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                isNew = false;

            }
        }

        public bool IsNew() => isNew;
    }
}