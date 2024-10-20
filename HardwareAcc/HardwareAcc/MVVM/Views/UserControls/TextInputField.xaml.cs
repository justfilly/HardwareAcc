using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HardwareAcc.MVVM.Views.UserControls;

public partial class TextInputField
{
    private readonly Binding _textBinding;
    public TextInputField()
    {
        InitializeComponent();
        
        ValidationRules = new ObservableCollection<ValidationRule>();
        
        _textBinding = BindingOperations.GetBinding(TextBox, TextBox.TextProperty)!;
        ValidationRules.CollectionChanged += ValidationRules_CollectionChanged!;
        Unloaded += TextInputField_Unloaded; 
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
    
    public static readonly DependencyProperty ErrorTextDependencyProperty =
        DependencyProperty.Register(
            name:nameof(ErrorText),
            propertyType:typeof(string),
            ownerType:typeof(TextInputField),
            new PropertyMetadata(string.Empty));
    
    public string ErrorText
    {
        get => (string)GetValue(ErrorTextDependencyProperty);
        set => SetValue(ErrorTextDependencyProperty, value);
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
            name: nameof(IsValid),
            propertyType: typeof(bool),
            ownerType: typeof(TextInputField),
            new PropertyMetadata(false));

    public bool IsValid
    {
        get => (bool)GetValue(HasErrorsDependencyProperty);
        set => SetValue(HasErrorsDependencyProperty, value);
    }

    private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsValid = !Validation.GetHasError(TextBox);

        if (Validation.GetErrors(TextBox).Count > 0)
            ErrorLabel.Text = (string)Validation.GetErrors(TextBox)[0].ErrorContent;
        else
            ErrorLabel.Text = string.Empty;
    }

    public static readonly DependencyProperty ValidationRulesProperty =
        DependencyProperty.Register(
            nameof(ValidationRules), 
            typeof(ObservableCollection<ValidationRule>), 
            typeof(TextInputField), 
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