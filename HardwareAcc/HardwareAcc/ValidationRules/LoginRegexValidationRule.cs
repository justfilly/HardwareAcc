using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class LoginRegexValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Login";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";
        
        if (RegexProvider.Login().IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} must contain only letters, digits, underscores, hyphens, dots");
        }

        return ValidationResult.ValidResult;
    }


}