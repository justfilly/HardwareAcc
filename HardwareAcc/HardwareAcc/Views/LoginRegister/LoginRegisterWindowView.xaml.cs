using HardwareAcc.Views.LoginRegister.Pages;

namespace HardwareAcc.Views.LoginRegister;

public partial class LoginRegisterWindowView
{
    public LoginRegisterWindowView()
    {
        InitializeComponent();
        MainFrame.Navigate(new LoginPageView());
    }
}