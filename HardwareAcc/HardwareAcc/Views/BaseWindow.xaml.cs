using HardwareAcc.ViewModels;

namespace HardwareAcc.Views;

public partial class BaseWindow : ChromeWindow
{
    public BaseWindow()
    {
        InitializeComponent();
        DataContext = new ChromeWindowViewModel(this);
    }
}