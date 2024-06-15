using HardwareAcc.ViewModels;

namespace HardwareAcc.Views;

public partial class TestWindow : ChromeWindow
{
    public TestWindow()
    {
        InitializeComponent();
        DataContext = new TestWindowViewModel(this);
    }
}