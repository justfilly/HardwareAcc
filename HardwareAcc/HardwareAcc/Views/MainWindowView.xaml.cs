using HardwareAcc.Services.AuthService;
using HardwareAcc.Views.Pages;

namespace HardwareAcc.Views;

public partial class MainWindowView
{
    public MainWindowView(IAuthService authService)
    {
        InitializeComponent();
        MainFrame.Navigate(new LoginPageView(authService));
    }
}