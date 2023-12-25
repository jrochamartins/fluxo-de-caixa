namespace FluxoDeCaixa.Domain.Models
{
    public class DailyBalance : Entity
    {
        public DateOnly Date { get; set; }

        public decimal Credits { get; set; }

        public decimal Debts { get; set; }

        public decimal Balance => Credits - Debts;
    }
}
