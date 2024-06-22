using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;

namespace HardwareAcc.ValidationRules;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
        {
            return new ValidationResult(false, "Field cannot be empty.");
        }
        
        return ValidationResult.ValidResult;
    }
}

public class TestValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value.ToString().Length < 3)
        {
            return new ValidationResult(false, "Field too short.");
        }
        
        return ValidationResult.ValidResult;
    }
}

public class MultiValidationRule : ValidationRule
{
    public List<ValidationRule> Rules { get; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        foreach (var rule in Rules)
        {
            var result = rule.Validate(value, cultureInfo);
            if (!result.IsValid)
            {
                return result;
            }
        }

        return ValidationResult.ValidResult;
    }
}