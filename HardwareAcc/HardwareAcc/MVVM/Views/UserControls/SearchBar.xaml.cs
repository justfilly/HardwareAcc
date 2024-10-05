using System.Windows;
using System.Windows.Data;

namespace HardwareAcc.MVVM.Views.UserControls;

public partial class SearchBar
{
    public SearchBar()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty InputTextDependencyProperty =
        DependencyProperty.Register(
            name:nameof(InputText),
            propertyType:typeof(string),
            ownerType:typeof(SearchBar),
            new PropertyMetadata(string.Empty));
    
    public string InputText
    {
        get => (string)GetValue(InputTextDependencyProperty);
        set => SetValue(InputTextDependencyProperty, value);
    }
}