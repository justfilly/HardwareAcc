using HardwareAcc.Commands;
using HardwareAcc.Services.AuthService;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Pages;

public class LoginPageViewModel : BaseViewModel
{
    public LoginPageViewModel(IAuthService authService, INavigationService navigationService)
    {
        LoginCommand = new LoginCommand(this, authService);
        RegisterPageNavigateCommand = new RegisterPageNavigateCommand(navigationService);
    }
    
    public LoginCommand LoginCommand { get; }
    public RegisterPageNavigateCommand RegisterPageNavigateCommand { get; }
    
    private string _username = "";
    public string Username
    {
        get => _username;
    
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
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
}