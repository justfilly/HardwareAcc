using System;
using System.Windows.Controls;
using System.Windows.Media;
using HardwareAcc.Views.UserControls;
using SharpVectors.Converters;

namespace HardwareAcc.Commands;

public class TogglePasswordVisibilityCommand : BaseCommand
{
    private readonly PasswordInputField _passwordInputField;
    
    private readonly TextBox _passwordInputBox;
    private readonly FontFamily _passwordFont;
    private readonly FontFamily _normalFont;
    
    private readonly SvgViewbox _visibilityIcon;
    private readonly Uri _hidePasswordIconUri;
    private readonly Uri _showPasswordIconUri;


    public TogglePasswordVisibilityCommand(PasswordInputField passwordInputField,
        TextBox passwordInputBox,
        FontFamily passwordFont,
        FontFamily normalFont,
        SvgViewbox visibilityIcon,
        Uri hidePasswordIconUri,
        Uri showPasswordIconUri)
    {
        _passwordInputField = passwordInputField;
        _passwordInputBox = passwordInputBox;
        _passwordFont = passwordFont;
        _normalFont = normalFont;
        _visibilityIcon = visibilityIcon;
        _hidePasswordIconUri = hidePasswordIconUri;
        _showPasswordIconUri = showPasswordIconUri;
    }

    public override void Execute(object? parameter)
    {
        _passwordInputField.PasswordVisibility = !_passwordInputField.PasswordVisibility;

        if (_passwordInputField.PasswordVisibility == true)
        {
            _passwordInputBox.FontFamily = _normalFont;
            _visibilityIcon.Source = _hidePasswordIconUri;
        }
        else
        {
            _passwordInputBox.FontFamily = _passwordFont;
            _visibilityIcon.Source = _showPasswordIconUri;
        }
    }
}