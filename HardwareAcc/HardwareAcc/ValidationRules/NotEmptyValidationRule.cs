using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class NotEmptyValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Field";
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
        {
            return new ValidationResult(false, $"{FieldName} cannot be empty");
        }
        
        return ValidationResult.ValidResult;
    }
}