using HardwareAcc.ViewModels.LoginRegister.Pages;

namespace HardwareAcc.Views.LoginRegister.Pages;

public partial class LoginPageView
{
    public LoginPageView()
    {
        DataContext = new LoginPageViewModel();
        InitializeComponent();
    }
}