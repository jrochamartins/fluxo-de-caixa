using FluxoDeCaixa.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class PostEntryRequest
    {
        public DateTime? Date { get; set; } = DateTime.Now;

        [Required]
        public EntryType EntryType { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string? Description { get; set; }

        [Required]
        [DecimalGreaterThan(0)]
        public double Value { get; set; }
    }
}
