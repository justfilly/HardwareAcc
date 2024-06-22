using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class PhoneNumberValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Phone Number";
    public bool IgnoreEmpty { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";
        
        if (IgnoreEmpty && string.IsNullOrEmpty(input))
        {
            return ValidationResult.ValidResult;
        }

        if (RegexProvider.PhoneNumber().IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} is not in a valid format");
        }

        return ValidationResult.ValidResult;
    }
}