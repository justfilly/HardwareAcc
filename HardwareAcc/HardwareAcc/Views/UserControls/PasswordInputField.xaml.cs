using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using HardwareAcc.Commands;

namespace HardwareAcc.Views.UserControls;

public partial class PasswordInputField
{
    public bool PasswordVisibility { get; set; }
    
    public PasswordInputField()
    {
        InitializeComponent();
        
        FontFamily passwordFont = (FontFamily)FindResource("Font_Password");
        FontFamily normalFont = (FontFamily)FindResource("Font_Inter");
        PasswordInputBox.FontFamily = passwordFont;
        
        Uri hidePasswordIconUri = new("/Resources/Icons/hide_password.svg", UriKind.Relative);
        Uri showPasswordIconUri = new("/Resources/Icons/show_password.svg", UriKind.Relative);
        TogglePasswordVisibilityIcon.Source = showPasswordIconUri;
        
        TogglePasswordVisibilityCommand = new TogglePasswordVisibilityCommand(this,
            PasswordInputBox,
            passwordFont,
            normalFont,
            TogglePasswordVisibilityIcon,
            hidePasswordIconUri,
            showPasswordIconUri);
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