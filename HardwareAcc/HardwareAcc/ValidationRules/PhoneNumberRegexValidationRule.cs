using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class PhoneNumberValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Phone Number";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = (value as string)!;

        Regex regex = new (@"^\+?\d{1,3}[-\s]?\d{3,}$");

        if (!regex.IsMatch(input))
        {
            return new ValidationResult(false, $"{FieldName} is not in a valid format");
        }

        return ValidationResult.ValidResult;
    }
}