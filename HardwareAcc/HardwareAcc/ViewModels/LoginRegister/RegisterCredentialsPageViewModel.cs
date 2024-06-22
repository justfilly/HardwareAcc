using HardwareAcc.Commands;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.LoginRegister;

public class RegisterCredentialsPageViewModel : BaseViewModel
{
    public RegisterCredentialsPageViewModel(INavigationService navigationService,
        IAuthService authService,
        RegisterNamePageViewModel registerNamePageViewModel,
        RegisterContactInfoPageViewModel registerContactInfoPageViewModel)
    {
        RegisterContactInfoNavigateCommand = new NavigateCommand<RegisterContactInfoPageViewModel>(navigationService);
        RegisterCommand = new RegisterCommand(authService,
            registerNamePageViewModel,
            registerContactInfoPageViewModel,
            this,
            navigationService);
    }
    
    public NavigateCommand<RegisterContactInfoPageViewModel> RegisterContactInfoNavigateCommand { get; }
    public RegisterCommand RegisterCommand { get; }
    
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
    
    private string _confirmPassword = "";
    public string ConfirmPassword
    {
        get => _confirmPassword;
    
        set
        {
            _confirmPassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
        }
    }
}