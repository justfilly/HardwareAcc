using HardwareAcc.Services.AuthService;
using HardwareAcc.ViewModels.LoginRegister.Pages;

namespace HardwareAcc.Views.LoginRegister.Pages;

public partial class LoginPageView
{
    public LoginPageView(IAuthService authService)
    {
        DataContext = new LoginPageViewModel(authService);
        InitializeComponent();
    }
}