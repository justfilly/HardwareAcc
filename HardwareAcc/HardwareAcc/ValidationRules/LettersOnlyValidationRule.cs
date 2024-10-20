using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class LettersOnlyValidationRule : ValidationRule
{
    public string FieldName { get; set; } = "Field";
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string ?? "";
        
        if (RegexProvider.LettersOnly().IsMatch(input) == false)
        {
            return new ValidationResult(false, $"{FieldName} must contain only letters");
        }

        return ValidationResult.ValidResult;
    }


}