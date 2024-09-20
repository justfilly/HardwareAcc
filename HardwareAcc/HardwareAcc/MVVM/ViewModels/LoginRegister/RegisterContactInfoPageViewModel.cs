using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.LoginRegister;

public class RegisterContactInfoPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public RegisterContactInfoPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        RegisterCredentialsNavigateCommand = new RelayCommand(NavigateToCredentialsPage, CanExecuteNavigateToCredentialsPage);
        RegisterNameNavigateCommand = new NavigateCommand<RegisterNamePageViewModel>(navigationService);
    }
    
    public RelayCommand RegisterCredentialsNavigateCommand { get; }
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
    
    private bool _isEmailValid = true;
    public bool IsEmailValid
    {
        get => _isEmailValid;
    
        set
        {
            _isEmailValid = value;
            OnPropertyChanged(nameof(IsEmailValid));
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
    
    private bool _isPhoneNumberValid = true;
    public bool IsPhoneNumberValid
    {
        get => _isPhoneNumberValid;
    
        set
        {
            _isPhoneNumberValid = value;
            OnPropertyChanged(nameof(IsPhoneNumberValid));
        }
    }
    
    private void NavigateToCredentialsPage() => 
        _navigationService.Navigate<RegisterCredentialsPageViewModel>();

    private bool CanExecuteNavigateToCredentialsPage()
    {
        return IsEmailValid && IsPhoneNumberValid;
    }

}