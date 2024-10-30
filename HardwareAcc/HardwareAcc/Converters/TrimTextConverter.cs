using System;
using System.Globalization;
using System.Windows.Data;

namespace HardwareAcc.Converters;

public class TrimTextConverter : IValueConverter
{
    public int MaxLength { get; set; } = 20;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string text && text.Length > MaxLength)
            return text.Substring(0, MaxLength) + "...";
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
}