using HardwareAcc.Views.UserControls;

namespace HardwareAcc.Commands;

public class TogglePasswordVisibilityCommand : BaseCommand
{
    private readonly PasswordInputField _passwordInputField;

    public TogglePasswordVisibilityCommand(PasswordInputField passwordInputField)
    {
        _passwordInputField = passwordInputField;
    }

    public override void Execute(object? parameter)
    {
        _passwordInputField.PasswordVisibility = !_passwordInputField.PasswordVisibility;
    }
}