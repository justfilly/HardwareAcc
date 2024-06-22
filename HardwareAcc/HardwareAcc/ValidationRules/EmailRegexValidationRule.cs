using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class EmailValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Email";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";
        
        if (RegexProvider.Email().IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} is not in a valid format");
        }

        return ValidationResult.ValidResult;
    }
}