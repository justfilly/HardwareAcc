using HardwareAcc.Commands;
using HardwareAcc.Services.AuthService;

namespace HardwareAcc.ViewModels.LoginRegister.Pages;

public class LoginPageViewModel : BaseViewModel
{
    public LoginPageViewModel(IAuthService authService)
    {
        LoginCommand = new LoginCommand(this, authService);
        RegisterNavigateCommand = new RegisterNavigateCommand();
    }
    
    public LoginCommand LoginCommand { get; }
    public RegisterNavigateCommand RegisterNavigateCommand { get; }
    
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
}