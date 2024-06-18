using System.Windows;

namespace HardwareAcc.Views;

public partial class InputUserControlView
{
    public InputUserControlView()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty InputTextDependencyProperty =
        DependencyProperty.Register(
            name:nameof(InputText),
            propertyType:typeof(string),
            ownerType:typeof(InputUserControlView),
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
            ownerType: typeof(InputUserControlView),
            new PropertyMetadata(string.Empty));

    public string LabelText
    {
        get => (string)GetValue(LabelTextDependencyProperty);
        set => SetValue(LabelTextDependencyProperty, value);
    }
}