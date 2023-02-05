using System.Windows.Controls;

//https://www.codeproject.com/Tips/690130/Simple-Validation-in-WPF
namespace CardDesigner.UI.Validators
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "value cannot be empty.");
            }
            else
            {
                if (value.ToString().Length > 3)
                {
                    return new ValidationResult
                    (false, "Name cannot be more than 3 characters long.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
