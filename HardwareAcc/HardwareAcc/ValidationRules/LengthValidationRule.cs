using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class LengthValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Field";
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public bool IgnoreEmpty { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";

        if (IgnoreEmpty && string.IsNullOrEmpty(input))
        {
            return ValidationResult.ValidResult;
        }

        string pattern = $@"^[\s\S]{{{MinLength},{MaxLength}}}$";
        Regex regex = new(pattern);

        if (regex.IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} must be between {MinLength} and {MaxLength} characters");
        }

        return ValidationResult.ValidResult;
    }
}