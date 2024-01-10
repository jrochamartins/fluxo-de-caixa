using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class GetBalanceRequest
    {
        [Required]
        [Range(1, 31)]
        public int Day { get; init; }

        [Required]
        [Range(1, 12)]
        public int Month { get; init; }

        [Required]
        [Range(1, 9999)]
        public int Year { get; init; }

        public DateOnly GetDateOnly() =>
            new(Year, Month, Day);

    }
}
