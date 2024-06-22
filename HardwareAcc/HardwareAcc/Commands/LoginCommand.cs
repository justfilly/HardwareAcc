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

    public override async void Execute(object? parameter)
    {
        if (_loginPageViewModel.IsLoginValid == true && _loginPageViewModel.IsPasswordValid == true)
        {
            string login = _loginPageViewModel.Login;
            string password = _loginPageViewModel.Password;
            
            bool isAuthenticated = await _authService.ValidateCredentialsAsync(login, password);

            if (isAuthenticated)
            {
                await _authService.LogInAsync(login, password);
                _navigationService.Navigate<AccountingPageViewModel>();
            }
        }
    }
}