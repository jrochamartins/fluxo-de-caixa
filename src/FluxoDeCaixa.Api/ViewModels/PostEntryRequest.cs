using FluxoDeCaixa.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class PostEntryRequest
    {
        public DateTime? Date { get; init; } = DateTime.Now;

        [Required]
        public EntryType EntryType { get; init; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string? Description { get; init; }

        [Required]
        [DecimalGreaterThan(0)]
        public double Value { get; init; }
    }
}
