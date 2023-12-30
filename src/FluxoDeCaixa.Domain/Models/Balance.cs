using FluxoDeCaixa.Domain.Abstractions.Models;

namespace FluxoDeCaixa.Domain.Models
{
    public class Balance : Entity
    {
        public DateOnly Date { get; set; }

        public decimal Credits { get; set; }

        public decimal Debts { get; set; }

        public decimal Value => Credits - Debts;
    }
}
