using System.Text.RegularExpressions;

namespace HardwareAcc.ValidationRules;

public static partial class RegexProvider
{
    [GeneratedRegex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
    public static partial Regex Email();
    
    [GeneratedRegex("^[a-zA-Zа-яА-Я]+$")]
    public static partial Regex LettersOnly();
    
    [GeneratedRegex("^[a-zA-Z0-9_\\-\\.]+$")]
    public static partial Regex Login();
    
    [GeneratedRegex("^[0-9]+$")]
    public static partial Regex NumbersOnly();
    
    [GeneratedRegex("^[a-zA-Z0-9!@#$%^&*()\\-_=+{}\\[\\]|\\\\;:'\",<.>/?]*$")]
    public static partial Regex Password();
    
    [GeneratedRegex("^\\+?\\d{1,3}[-\\s]?\\d{3,}$")]
    public static partial Regex PhoneNumber();
}