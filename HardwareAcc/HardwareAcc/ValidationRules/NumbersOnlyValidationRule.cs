using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class NumbersOnlyValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Field";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";

        if (RegexProvider.NumbersOnly().IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} must contain only numbers");
        }

        return ValidationResult.ValidResult;
    }


}