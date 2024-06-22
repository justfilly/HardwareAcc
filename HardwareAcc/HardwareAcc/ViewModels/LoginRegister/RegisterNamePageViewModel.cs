using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.LoginRegister;

public class RegisterNamePageViewModel : BaseViewModel
{
    public RegisterNamePageViewModel(INavigationService navigationService)
    {
        
        RegisterContactInfoNavigateCommand = new NavigateCommand<RegisterContactInfoPageViewModel>(navigationService);
        LoginNavigateCommand = new NavigateCommand<LoginPageViewModel>(navigationService);
    }

    public NavigateCommand<RegisterContactInfoPageViewModel> RegisterContactInfoNavigateCommand { get; }
    public NavigateCommand<LoginPageViewModel> LoginNavigateCommand { get; }
    
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