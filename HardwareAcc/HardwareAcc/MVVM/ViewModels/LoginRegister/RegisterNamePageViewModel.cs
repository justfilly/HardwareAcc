using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.LoginRegister;

public class RegisterNamePageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public RegisterNamePageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        RegisterContactInfoNavigateCommand = 
            new RelayCommand(NavigateToContactInfoPage, CanExecuteNavigateToContactInfoPage);
        
        LoginNavigateCommand = new NavigateCommand<LoginPageViewModel>(navigationService);
    }
    
    public RelayCommand RegisterContactInfoNavigateCommand { get; }
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
    
    private bool _isFirstNameValid;
    public bool IsFirstNameValid
    {
        get => _isFirstNameValid;
    
        set
        {
            _isFirstNameValid = value;
            OnPropertyChanged(nameof(IsFirstNameValid));
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
    
    private bool _isSecondNameValid;
    public bool IsSecondNameValid
    {
        get => _isSecondNameValid;
    
        set
        {
            _isSecondNameValid = value;
            OnPropertyChanged(nameof(IsSecondNameValid));
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

    private bool _isPatronymicValid;
    public bool IsPatronymicValid
    {
        get => _isPatronymicValid;
    
        set
        {
            _isPatronymicValid = value;
            OnPropertyChanged(nameof(IsPatronymicValid));
        }
    }
    
    private void NavigateToContactInfoPage() => 
        _navigationService.Navigate<RegisterContactInfoPageViewModel>();

    private bool CanExecuteNavigateToContactInfoPage() => 
        IsFirstNameValid && IsSecondNameValid && IsPatronymicValid;
}