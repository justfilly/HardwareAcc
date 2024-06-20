using System.Windows;

namespace HardwareAcc.Views.UserControls;

public partial class PasswordInputField
{
    public PasswordInputField()
    {
        InitializeComponent();
    }
    
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
}