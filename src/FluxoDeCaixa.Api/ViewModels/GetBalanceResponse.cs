namespace FluxoDeCaixa.Api.ViewModels
{
    public class GetBalanceResponse
    {
        public DateOnly Date { get; set; }

        public decimal Credits { get; set; }

        public decimal Debts { get; set; }

        public decimal Value { get; set; }
    }
}
