using HardwareAcc.Services.AuthService;
using HardwareAcc.Views.LoginRegister.Pages;

namespace HardwareAcc.Views.LoginRegister;

public partial class LoginRegisterWindowView
{
    public LoginRegisterWindowView(IAuthService authService)
    {
        InitializeComponent();
        MainFrame.Navigate(new LoginPageView(authService));
    }
}