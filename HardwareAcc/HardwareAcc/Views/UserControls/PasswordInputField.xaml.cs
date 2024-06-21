using System.Windows;
using System.Windows.Input;
using HardwareAcc.Commands;

namespace HardwareAcc.Views.UserControls;

public partial class PasswordInputField
{
    public bool PasswordVisibility = false;
    
    public PasswordInputField()
    {
        InitializeComponent();
        TogglePasswordVisibilityCommand = new TogglePasswordVisibilityCommand(this);
    }

    public TogglePasswordVisibilityCommand TogglePasswordVisibilityCommand { get; }

    public static readonly DependencyProperty InputTextDependencyProperty =
        DependencyProperty.Register(
            name:nameof(PasswordText),
            propertyType:typeof(string),
            ownerType:typeof(PasswordInputField),
            new PropertyMetadata(string.Empty));

    public string PasswordText
    {
        get => (string)GetValue(InputTextDependencyProperty);
        set => SetValue(InputTextDependencyProperty, value);
    }

    public static readonly DependencyProperty LabelTextDependencyProperty =
        DependencyProperty.Register(
            name: nameof(LabelText),
            propertyType: typeof(string),
            ownerType: typeof(PasswordInputField),
            new PropertyMetadata(string.Empty));

    public string LabelText
    {
        get => (string)GetValue(LabelTextDependencyProperty);
        set => SetValue(LabelTextDependencyProperty, value);
    }

    private void HandleCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if ( e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy) 
        {
            e.CanExecute = PasswordVisibility;
            e.Handled = true;
        }
    }
}