using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HardwareAcc.Views.UserControls;

public partial class TextInputField
{
    public TextInputField()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty InputTextDependencyProperty =
        DependencyProperty.Register(
            name:nameof(InputText),
            propertyType:typeof(string),
            ownerType:typeof(TextInputField),
            new PropertyMetadata(string.Empty));
    
    public string InputText
    {
        get => (string)GetValue(InputTextDependencyProperty);
        set => SetValue(InputTextDependencyProperty, value);
    }

    public static readonly DependencyProperty LabelTextDependencyProperty =
        DependencyProperty.Register(
            name: nameof(LabelText),
            propertyType: typeof(string),
            ownerType: typeof(TextInputField),
            new PropertyMetadata(string.Empty));

    public string LabelText
    {
        get => (string)GetValue(LabelTextDependencyProperty);
        set => SetValue(LabelTextDependencyProperty, value);
    }

    public static readonly DependencyProperty HasErrorsDependencyProperty =
        DependencyProperty.Register(
            name: nameof(HasErrors),
            propertyType: typeof(bool),
            ownerType: typeof(TextInputField),
            new PropertyMetadata(false));

    public bool HasErrors
    {
        get => (bool)GetValue(HasErrorsDependencyProperty);
        set => SetValue(HasErrorsDependencyProperty, value);
    }
    
    private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        HasErrors = Validation.GetHasError(TextBox);

        if (Validation.GetErrors(TextBox).Count > 0)
            ErrorLabel.Content = (string)Validation.GetErrors(TextBox)[0].ErrorContent;
        else
            ErrorLabel.Content = string.Empty;
    }
}