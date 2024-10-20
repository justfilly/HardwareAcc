using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class RegexValidationRule : ValidationRule
{
    public string Pattern { get; set; } = "";
    public string Message { get; set; } = "";

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = (value as string)!;
        
        Regex regex = new(Pattern);
        
        if (regex.IsMatch(input) == false)
        {
            return new ValidationResult(false, Message);
        }

        return ValidationResult.ValidResult;
    }
}