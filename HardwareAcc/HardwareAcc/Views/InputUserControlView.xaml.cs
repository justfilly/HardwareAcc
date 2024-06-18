using System.Windows;

namespace HardwareAcc.Views;

public partial class InputUserControlView
{
    public InputUserControlView()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty InputTextProperty =
        DependencyProperty.Register(
            name:nameof(InputText),
            propertyType:typeof(string),
            ownerType:typeof(InputUserControlView),
            new PropertyMetadata(string.Empty));
    
    public string InputText
    {
        get => (string)GetValue(InputTextProperty);
        set => SetValue(InputTextProperty, value);
    }
}