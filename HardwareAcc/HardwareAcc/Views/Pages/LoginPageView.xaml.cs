using HardwareAcc.Services.AuthService;
using HardwareAcc.ViewModels.Pages;

namespace HardwareAcc.Views.Pages;

public partial class LoginPageView
{
    public LoginPageView(IAuthService authService)
    {
        DataContext = new LoginPageViewModel(authService);
        InitializeComponent();
    }
}