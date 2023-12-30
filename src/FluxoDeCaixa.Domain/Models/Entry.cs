using FluxoDeCaixa.Domain.Abstractions.Models;

namespace FluxoDeCaixa.Domain.Models
{
    public class Entry : Entity
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        public EntryType EntryType { get; set; }

        public decimal Value { get; set; }
    }
}
