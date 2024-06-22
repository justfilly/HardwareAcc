using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HardwareAcc.Converters;

public class ValidationRulesConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var validationRules = new List<ValidationRule>();
        foreach (var value in values)
        {
            if (value is IEnumerable<ValidationRule> rules)
            {
                validationRules.AddRange(rules);
            }
        }
        return validationRules;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}