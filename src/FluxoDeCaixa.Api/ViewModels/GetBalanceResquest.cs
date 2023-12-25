using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class GetBalanceResquest
    {
        [Required]
        [Range(1, 31)]
        public int Day { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [Range(1, 9999)]
        public int Year { get; set; }

        public DateOnly GetDateOnly() =>
            new(Year, Month, Day);

    }
}
