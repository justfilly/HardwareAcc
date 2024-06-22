using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class PasswordValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Password";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = (value as string)!;
        Regex regex = new(@"^[a-zA-Z0-9!@#$%^&*()\-_=+{}\[\]|\\;:'"",<.>/?]*$");

        if (!regex.IsMatch(input))
        {
            return new ValidationResult(false, $"{FieldName} must contain a combination of letters, numbers, and symbols.");
        }

        return ValidationResult.ValidResult;
    } 
}