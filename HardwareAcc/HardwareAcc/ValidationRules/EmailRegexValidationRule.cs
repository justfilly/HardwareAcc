using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class EmailValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Email";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = (value as string)!;

        Regex regex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        if (regex.IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} is not in a valid format");
        }

        return ValidationResult.ValidResult;
    }
}