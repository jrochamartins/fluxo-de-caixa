using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class DecimalGreaterThanAttribute(double val) : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;
            return Convert.ToDecimal(value) > (decimal)val;
        }
    }
}
