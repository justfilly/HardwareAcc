using System.Windows;
using HardwareAcc.ViewModels;

namespace HardwareAcc.Views;

public partial class InputUserControlView
{
    public InputUserControlViewModel _viewModel { get; }

    public InputUserControlView()
    {
        InitializeComponent();
        _viewModel = new InputUserControlViewModel();
        DataContext = _viewModel;
    }
    
    public static readonly DependencyProperty InputTextProperty =
        DependencyProperty.Register("InputText", typeof(string), typeof(InputUserControlView), new PropertyMetadata(string.Empty));
    public string InputText
    {
        get => (string)GetValue(InputTextProperty);
        set => SetValue(InputTextProperty, value);
    }

}