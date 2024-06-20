using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Pages;

public class RegisterNamePageViewModel : BaseViewModel
{
    public RegisterNamePageViewModel(INavigationService navigationService)
    {
        
        RegisterContactInfoNavigateCommand = new NavigationCommand<RegisterContactInfoPageViewModel>(navigationService);
        LoginNavigateCommand = new NavigationCommand<LoginPageViewModel>(navigationService);
    }

    public NavigationCommand<RegisterContactInfoPageViewModel> RegisterContactInfoNavigateCommand { get; }
    public NavigationCommand<LoginPageViewModel> LoginNavigateCommand { get; }
    
    private string _firstName = "";
    public string FirstName
    {
        get => _firstName;
    
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }
    
    private string _secondName = "";
    public string SecondName
    {
        get => _secondName;
    
        set
        {
            _secondName = value;
            OnPropertyChanged(nameof(SecondName));
        }
    }

    private string _patronymic = "";
    public string Patronymic
    {
        get => _patronymic;
    
        set
        {
            _patronymic = value;
            OnPropertyChanged(nameof(Patronymic));
        }
    }
    
    
}