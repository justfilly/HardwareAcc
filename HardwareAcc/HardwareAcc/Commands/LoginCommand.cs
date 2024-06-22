using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels;
using HardwareAcc.ViewModels.LoginRegister;

namespace HardwareAcc.Commands;

public class LoginCommand : BaseCommand
{
    private readonly LoginPageViewModel _loginPageViewModel;
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;

    public LoginCommand(LoginPageViewModel loginPageViewModel, IAuthService authService, INavigationService navigationService)
    {
        _loginPageViewModel = loginPageViewModel;
        _authService = authService;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        bool isValid = _authService.ValidateCredentialsAsync(_loginPageViewModel.Login, _loginPageViewModel.Password).Result;

        if (isValid)
        {
            _navigationService.Navigate<AccountingPageViewModel>();
        }
    }
}