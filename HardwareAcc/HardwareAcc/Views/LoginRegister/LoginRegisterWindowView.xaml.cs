using HardwareAcc.Views.LoginRegister.Pages;

namespace HardwareAcc.Views.LoginRegister;

public partial class LoginRegisterWindowView
{
    public LoginRegisterWindowView(LoginPageView loginPageView)
    {
        InitializeComponent();
        MainFrame.Navigate(loginPageView);
    }
}