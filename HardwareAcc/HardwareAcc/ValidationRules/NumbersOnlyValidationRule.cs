using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class NumbersOnlyValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Field";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = (value as string)!;

        Regex regex = new Regex(@"^[0-9]+$");

        if (!regex.IsMatch(input))
        {
            return new ValidationResult(false, $"{FieldName} must contain only numbers");
        }

        return ValidationResult.ValidResult;
    }
}