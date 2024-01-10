namespace FluxoDeCaixa.Api.ViewModels
{
    public class GetBalanceResponse
    {
        public DateOnly Date { get; init; }

        public decimal Credits { get; init; }

        public decimal Debts { get; init; }

        public decimal Value { get; init; }
    }
}
