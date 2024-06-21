using System.Diagnostics;
using HardwareAcc.Commands;
using HardwareAcc.Services.AuthService;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Pages;

public class LoginPageViewModel : BaseViewModel
{
    public LoginPageViewModel(IAuthService authService, INavigationService navigationService)
    {
        LoginCommand = new LoginCommand(this, authService);
        RegisterNavigateCommand = new NavigateCommand<RegisterNamePageViewModel>(navigationService);
    }
    
    public LoginCommand LoginCommand { get; }
    public NavigateCommand<RegisterNamePageViewModel> RegisterNavigateCommand { get; }
    
    private string _login = "";
    public string Login
    {
        get => _login;
    
        set
        {
            _login = value;
            OnPropertyChanged(nameof(Login));
        }
    }
    
    private string _password = "";
    public string Password
    {
        get => _password;
    
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private bool _isValidLogin;
    public bool IsValidLogin
    {
        get
        {
            Debug.WriteLine(_isValidLogin);
            return _isValidLogin;
        }

        set
        {
            Debug.WriteLine(value);
            _isValidLogin = value;
            OnPropertyChanged(nameof(IsValidLogin));
        }
    }
}