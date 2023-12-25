using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class DecimalGreaterThanAttribute(double val) : ValidationAttribute
    {
        private readonly decimal _val = (decimal)val;

        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;
            return Convert.ToDecimal(value) > _val;
        }

        //public override string FormatErrorMessage(string name)
        //{
        //    return String.Format(CultureInfo.CurrentCulture,
        //      ErrorMessageString, name);
        //}
    }
}
