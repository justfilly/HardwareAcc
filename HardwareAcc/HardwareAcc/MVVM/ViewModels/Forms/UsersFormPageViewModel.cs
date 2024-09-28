using System.Collections.ObjectModel;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.Forms;

public class UsersFormPageViewModel : BaseFormViewModel<UserModel>
{
    private readonly IUserRepository _repository;
    private readonly INavigationService _navigationService;

    private string _initialCode = "";
    
    public UsersFormPageViewModel(IUserRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _navigationService = navigationService;
        
        AccountingNavigateCommand = new NavigateCommand<AccountingPageViewModel>(navigationService);
        SubmitCommand = new RelayCommand(Submit, CanSubmit);
    }

    public NavigateCommand<AccountingPageViewModel> AccountingNavigateCommand { get; }
    public RelayCommand SubmitCommand { get; }

    #region FieldProperties
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
    
    private bool _isLoginValid;
    public bool IsLoginValid
    {
        get => _isLoginValid;
    
        set
        {
            _isLoginValid = value;
            OnPropertyChanged(nameof(IsLoginValid));
        }
    }
    
    private string _loginErrorText = "";
    public string LoginErrorText
    {
        get => _loginErrorText;
    
        set
        {
            _loginErrorText = value;
            OnPropertyChanged(nameof(LoginErrorText));
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
    
    private bool _isPasswordValid;
    public bool IsPasswordValid
    {
        get => _isPasswordValid;
    
        set
        {
            _isPasswordValid = value;
            OnPropertyChanged(nameof(IsPasswordValid));
        }
    }
    
    private string _passwordErrorText = "";
    public string PasswordErrorText
    {
        get => _passwordErrorText;
    
        set
        {
            _passwordErrorText = value;
            OnPropertyChanged(nameof(PasswordErrorText));
        }
    }
    
    private ObservableCollection<string> _roleItems;
    public ObservableCollection<string> RoleItems
    {
        get => _roleItems;
    
        set
        {
            _roleItems = value;
            OnPropertyChanged(nameof(RoleItems));
        }
    }
    
    private string _roleSelectedItem = "";
    public string RoleSelectedItem
    {
        get => _roleSelectedItem;
    
        set
        {
            _roleSelectedItem = value;
            OnPropertyChanged(nameof(RoleSelectedItem));
        }
    }

    #endregion

    public override void Initialize(UserModel model)
    {
        base.Initialize(model);
        
        RoleItems = new ObservableCollection<string> { "Option 1", "Option 2", "Option 3", "Optionwgahgreh fwegfeggdg3", "Optiowqfqwfn 3", "On3"};
    }

    private void Submit()
    {
        
    }

    private bool CanSubmit()
    {
        return false;
    }
}