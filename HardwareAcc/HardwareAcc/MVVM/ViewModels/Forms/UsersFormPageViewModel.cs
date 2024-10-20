using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Accounting;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Role;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.Forms;

public class UsersFormPageViewModel : BaseFormViewModel<UserModel>
{
    private readonly IUserRepository _userRepository;
    private readonly INavigationService _navigationService;
    private readonly IRoleRepository _roleRepository;

    private string _initialLogin = "";
    private string _initialEmail = "";
    private string _initialPhoneNumber = "";
    
    public UsersFormPageViewModel(IUserRepository userRepository, INavigationService navigationService, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _navigationService = navigationService;
        _roleRepository = roleRepository;

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

    private ObservableCollection<string> _roleItems = new();
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
    
    private string _roleErrorText = "";
    public string RoleErrorText
    {
        get => _roleErrorText;
    
        set
        {
            _roleErrorText = value;
            OnPropertyChanged(nameof(RoleErrorText));
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
    
    private bool _isPhoneNumberValid;
    public bool IsPhoneNumberValid
    {
        get => _isPhoneNumberValid;
    
        set
        {
            _isPhoneNumberValid = value;
            OnPropertyChanged(nameof(IsPhoneNumberValid));
        }
    }
    
    private string _phoneNumberErrorText = "";
    public string PhoneNumberErrorText
    {
        get => _phoneNumberErrorText;
    
        set
        {
            _phoneNumberErrorText = value;
            OnPropertyChanged(nameof(PhoneNumberErrorText));
        }
    }
    
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
    
    private bool _isEmailValid;
    public bool IsEmailValid
    {
        get => _isEmailValid;
    
        set
        {
            _isEmailValid = value;
            OnPropertyChanged(nameof(IsEmailValid));
        }
    }
    
    private string _emailErrorText = "";
    public string EmailErrorText
    {
        get => _emailErrorText;
    
        set
        {
            _emailErrorText = value;
            OnPropertyChanged(nameof(EmailErrorText));
        }
    }
    
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

    #endregion

    public override async void Initialize(UserModel model)
    {
        base.Initialize(model);

        int? id = model?.Id;

        Login = "";
        Password = "";
        RoleSelectedItem = "";
        Email = "";
        PhoneNumber = "";
        FirstName = "";
        SecondName = "";
        Patronymic = "";

        if (id == 0) 
        {
            _mode = FormMode.Add;

            IsLoginValid = false;
            _initialLogin = "";

            IsPasswordValid = false;

            IsEmailValid = false;
            _initialEmail = "";

            IsPhoneNumberValid = false;
            _initialPhoneNumber = "";
            
            IsFirstNameValid = false;
            IsSecondNameValid = false;
            IsPatronymicValid = false;
        }
        else
        {
            _initialLogin = model?.Login;
            _initialEmail = model?.Email;
            _initialPhoneNumber = model?.PhoneNumber;

            _mode = FormMode.Edit;

            Login = model?.Login;
            Password = model?.Password;
            Email = model?.Email;
            PhoneNumber = model?.PhoneNumber;
            FirstName = model?.FirstName;
            SecondName = model?.SecondName;
            Patronymic = model?.Password;

            IsLoginValid = true;
            IsPasswordValid = true;
            IsEmailValid = true;
            IsPhoneNumberValid = true;
            IsFirstNameValid = true;
            IsSecondNameValid = true;
            IsPatronymicValid = true;
        }
        
        await ResetRoleItems();

        RoleSelectedItem = _model?.RoleName;
        RoleErrorText = "";
    }

    private async Task ResetRoleItems()
    {
        RoleItems.Clear();
        IEnumerable<RoleModel> roleModels = await _roleRepository.GetAllAsync();
        foreach (RoleModel roleModel in roleModels) 
            RoleItems.Add(roleModel.Name);
    }

    private async void Submit()
    {
        if (IsRoleEmpty() == true)
            return;

        if (await IsLoginUnique() == false)
            return;
                
        if (await IsEmailUnique() == false)
            return;
                
        if (await IsPhoneNumberUnique() == false)
            return;

        RoleModel roleModel = await _roleRepository.GetByNameAsync(RoleSelectedItem);
        
        _model.Login = Login;
        _model.Password = Password;
        _model.RoleName = roleModel.Name;
        _model.RoleId = roleModel.Id;
        _model.Email = Email;
        _model.PhoneNumber = PhoneNumber;
        _model.FirstName = FirstName;
        _model.SecondName = SecondName;
        _model.Patronymic = Patronymic;
        
        if (_mode == FormMode.Add)
            await _userRepository.AddAsync(_model);
        else
            await _userRepository.UpdateAsync(_model);

        _navigationService.Navigate<AccountingPageViewModel>();
    }

    private bool CanSubmit()
    {
        return IsLoginValid &&
               IsPatronymicValid &&
               IsEmailValid &&
               IsPhoneNumberValid &&
               IsFirstNameValid &&
               IsSecondNameValid &&
               IsPatronymicValid;
    }

    private bool IsRoleEmpty()
    {
        if (string.IsNullOrEmpty(RoleSelectedItem) == true)
        {
            RoleErrorText = "Role is not selected";
            return true;
        }

        return false;
    }
    
    private async Task<bool> IsLoginUnique()
    {
        if (_mode == FormMode.Edit && _initialLogin == Login)
            return true;
        
        if (await _userRepository.GetByLoginAsync(Login) != null) 
        {
            LoginErrorText = "Login is not unique";
            return false;
        }

        return true;
    }

    private async Task<bool> IsEmailUnique()
    {
        if (_mode == FormMode.Edit && _initialEmail == Email)
            return true;
        
        if (await _userRepository.GetByEmailAsync(Email) != null) 
        {
            EmailErrorText = "Email is not unique";
            return false;
        }

        return true;
    }

    private async Task<bool> IsPhoneNumberUnique()
    {
        if (_mode == FormMode.Edit && _initialPhoneNumber == PhoneNumber)
            return true;
        
        if (await _userRepository.GetByPhoneNumberAsync(PhoneNumber) != null) 
        {
            PhoneNumberErrorText = "PhoneNumber is not unique";
            return false;
        }

        return true;
    }
}