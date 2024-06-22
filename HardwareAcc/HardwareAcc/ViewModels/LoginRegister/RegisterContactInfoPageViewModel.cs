using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.LoginRegister;

public class RegisterContactInfoPageViewModel : BaseViewModel
{
    public RegisterContactInfoPageViewModel(INavigationService navigationService)
    {
        RegisterCredentialsNavigateCommand = new NavigateCommand<RegisterCredentialsPageViewModel>(navigationService);
        RegisterNameNavigateCommand = new NavigateCommand<RegisterNamePageViewModel>(navigationService);
    }

    public NavigateCommand<RegisterCredentialsPageViewModel> RegisterCredentialsNavigateCommand { get; }
    public NavigateCommand<RegisterNamePageViewModel> RegisterNameNavigateCommand { get; }
    
    private string _email = "";
    public string Email
    {
        get => _email;
    
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    
    private string _phoneNumber = "";
    public string PhoneNumber
    {
        get => _phoneNumber;
    
        set
        {
            _phoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }
}