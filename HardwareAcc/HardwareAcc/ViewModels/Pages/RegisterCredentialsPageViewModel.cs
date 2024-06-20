using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Pages;

public class RegisterCredentialsPageViewModel : BaseViewModel
{
    public RegisterCredentialsPageViewModel(INavigationService navigationService)
    {
        RegisterContactInfoNavigateCommand = new NavigateCommand<RegisterContactInfoPageViewModel>(navigationService);
        RegisterCommand = new RegisterCommand();
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