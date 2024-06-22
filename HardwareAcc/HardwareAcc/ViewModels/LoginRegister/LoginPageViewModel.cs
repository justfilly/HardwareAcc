using HardwareAcc.Commands;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.LoginRegister;

public class LoginPageViewModel : BaseViewModel
{
    public LoginPageViewModel(IAuthService authService, INavigationService navigationService)
    {
        LoginCommand = new LoginCommand(this, authService, navigationService);
        RegisterNavigateCommand = new NavigateCommand<RegisterNamePageViewModel>(navigationService);
    }
    
    public LoginCommand LoginCommand { get; }
    public NavigateCommand<RegisterNamePageViewModel> RegisterNavigateCommand { get; }
    
    private string _login = "ponychek";
    public string Login
    {
        get => _login;
    
        set
        {
            _login = value;
            OnPropertyChanged(nameof(Login));
        }
    }
    
    private bool _isLoginValid;
    public bool IsLoginValid
    {
        get => _isLoginValid;

        set
        {
            _isLoginValid = value;
            OnPropertyChanged(nameof(IsLoginValid));
        }
    }
    
    private string _password = "12345678";
    public string Password
    {
        get => _password;
    
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private bool _isPasswordValid;
    public bool IsPasswordValid
    {
        get => _isPasswordValid;
    
        set
        {
            _isPasswordValid = value;
            OnPropertyChanged(nameof(IsPasswordValid));
        }
    }
}