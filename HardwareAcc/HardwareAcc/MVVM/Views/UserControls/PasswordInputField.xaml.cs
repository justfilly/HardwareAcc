using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using HardwareAcc.Commands;

namespace HardwareAcc.MVVM.Views.UserControls;

public partial class PasswordInputField
{
    private readonly Binding _textBinding;
    public bool PasswordVisibility { get; set; }
    
    public PasswordInputField()
    {
        InitializeComponent();
        
        ValidationRules = new ObservableCollection<ValidationRule>();
        
        _textBinding = BindingOperations.GetBinding(PasswordTextBox, TextBox.TextProperty)!;
        ValidationRules.CollectionChanged += ValidationRules_CollectionChanged!;
        Unloaded += TextInputField_Unloaded; 
        
        FontFamily passwordFont = (FontFamily)FindResource("Font_Password");
        FontFamily normalFont = (FontFamily)FindResource("Font_Inter");
        PasswordTextBox.FontFamily = passwordFont;
        
        Uri hidePasswordIconUri = new("/Resources/Icons/hide_password.svg", UriKind.Relative);
        Uri showPasswordIconUri = new("/Resources/Icons/show_password.svg", UriKind.Relative);
        TogglePasswordVisibilityIcon.Source = showPasswordIconUri;
        
        TogglePasswordVisibilityCommand = new TogglePasswordVisibilityCommand(this,
            PasswordTextBox,
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
    
    public static readonly DependencyProperty HasErrorsDependencyProperty =
        DependencyProperty.Register(
            name: nameof(IsValid),
            propertyType: typeof(bool),
            ownerType: typeof(PasswordInputField),
            new PropertyMetadata(false));

    public bool IsValid
    {
        get => (bool)GetValue(HasErrorsDependencyProperty);
        set => SetValue(HasErrorsDependencyProperty, value);
    }


    private void PasswordTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsValid = !Validation.GetHasError(PasswordTextBox);

        if (Validation.GetErrors(PasswordTextBox).Count > 0)
            ErrorLabel.Text = (string)Validation.GetErrors(PasswordTextBox)[0].ErrorContent;
        else
            ErrorLabel.Text = string.Empty;
    }
    
    public static readonly DependencyProperty ValidationRulesProperty =
        DependencyProperty.Register(
            nameof(ValidationRules), 
            typeof(ObservableCollection<ValidationRule>), 
            typeof(PasswordInputField), 
            new PropertyMetadata(null));
    
    public ObservableCollection<ValidationRule> ValidationRules
    {
        get => (ObservableCollection<ValidationRule>)GetValue(ValidationRulesProperty);
        set => SetValue(ValidationRulesProperty, value);
    }
    
    private void ValidationRules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => 
        UpdateValidationRules();

    private void UpdateValidationRules()
    {
        _textBinding.ValidationRules.Clear();        
        
        foreach (ValidationRule validationRule in ValidationRules) 
            _textBinding.ValidationRules.Add(validationRule);
    }
    
    private void TextInputField_Unloaded(object sender, RoutedEventArgs e)
    {
        ValidationRules.CollectionChanged -= ValidationRules_CollectionChanged!;
        Unloaded -= TextInputField_Unloaded;
    }
}