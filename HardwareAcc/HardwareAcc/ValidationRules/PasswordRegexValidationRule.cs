using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class PasswordValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Password";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";

        if (RegexProvider.Password().IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} must contain a combination of letters, numbers, and symbols.");
        }

        return ValidationResult.ValidResult;
    }
}