using System.Windows.Controls;
using System.Windows.Media;
using HardwareAcc.Views.UserControls;

namespace HardwareAcc.Commands;

public class TogglePasswordVisibilityCommand : BaseCommand
{
    private readonly PasswordInputField _passwordInputField;
    private readonly TextBox _passwordInputBox;
    private readonly FontFamily _passwordFont;
    private readonly FontFamily _normalFont;


    public TogglePasswordVisibilityCommand(PasswordInputField passwordInputField,
        TextBox passwordInputBox,
        FontFamily passwordFont,
        FontFamily normalFont)
    {
        _passwordInputField = passwordInputField;
        _passwordInputBox = passwordInputBox;
        _passwordFont = passwordFont;
        _normalFont = normalFont;
    }

    public override void Execute(object? parameter)
    {
        _passwordInputField.PasswordVisibility = !_passwordInputField.PasswordVisibility;

        if (_passwordInputField.PasswordVisibility == true)
            _passwordInputBox.FontFamily = _normalFont;
        else
            _passwordInputBox.FontFamily = _passwordFont;
    }
}