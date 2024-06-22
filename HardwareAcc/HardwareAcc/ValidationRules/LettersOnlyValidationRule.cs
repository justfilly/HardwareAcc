using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class LettersOnlyValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Field";
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = (value as string)!;
        
        Regex regex = new("^[a-zA-Zа-яА-Я]+$");
        
        if (regex.IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} must contain only letters");
        }

        return ValidationResult.ValidResult;
    }
}