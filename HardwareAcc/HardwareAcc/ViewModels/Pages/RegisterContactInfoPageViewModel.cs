using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Pages;

public class RegisterContactInfoPageViewModel : BaseViewModel
{
    public RegisterContactInfoPageViewModel(INavigationService navigationService)
    {
        RegisterNameNavigateCommand = new NavigationCommand<RegisterNamePageViewModel>(navigationService);
    }

    public NavigationCommand<RegisterNamePageViewModel> RegisterNameNavigateCommand { get; }
    
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