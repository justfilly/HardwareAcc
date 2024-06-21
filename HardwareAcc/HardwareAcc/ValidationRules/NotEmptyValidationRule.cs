using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
        {
            return new ValidationResult(false, "Field cannot be empty.");
        }
        
        return ValidationResult.ValidResult;
    }
}