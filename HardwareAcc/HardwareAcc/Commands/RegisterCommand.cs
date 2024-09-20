using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.LoginRegister;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.Commands;

public class RegisterCommand : BaseCommand
{
    private readonly IAuthService _authService;
    private readonly RegisterNamePageViewModel _registerNamePageViewModel;
    private readonly RegisterContactInfoPageViewModel _registerContactInfoPageViewModel;
    private readonly RegisterCredentialsPageViewModel _registerCredentialsPageViewModel;
    private readonly INavigationService _navigationService;

    public RegisterCommand(IAuthService authService,
        RegisterNamePageViewModel registerNamePageViewModel,
        RegisterContactInfoPageViewModel registerContactInfoPageViewModel,
        RegisterCredentialsPageViewModel registerCredentialsPageViewModel, 
        INavigationService navigationService)
    {
        _authService = authService;
        _registerNamePageViewModel = registerNamePageViewModel;
        _registerContactInfoPageViewModel = registerContactInfoPageViewModel;
        _registerCredentialsPageViewModel = registerCredentialsPageViewModel;
        _navigationService = navigationService;
    }
    
    public override async void Execute(object? parameter)
    {
        if (IsUserDataValid())
        {
            bool isRegisterCredentialsValid = await _authService.ValidateRegisterCredentialsAsync(
                _registerCredentialsPageViewModel.Login,
                _registerContactInfoPageViewModel.Email,
                _registerContactInfoPageViewModel.PhoneNumber
            );

            if (isRegisterCredentialsValid)
            {
                UserModel userModel = new UserModel
                {
                    RoleId = 2,
                
                    FirstName = _registerNamePageViewModel.FirstName,
                    SecondName = _registerNamePageViewModel.SecondName,
                    Patronymic = _registerNamePageViewModel.Patronymic,
           
                    Login = _registerCredentialsPageViewModel.Login,
                    Password = _registerCredentialsPageViewModel.Password,

                    Email = _registerContactInfoPageViewModel.Email,
                    PhoneNumber = _registerContactInfoPageViewModel.PhoneNumber
                };
       
                await _authService.RegisterAsync(userModel);
                _navigationService.Navigate<LoginPageViewModel>();
            }
        }
    }

    private bool IsUserDataValid()
    {
        bool isNameValid =
            _registerNamePageViewModel is { IsFirstNameValid: true, IsSecondNameValid: true, IsPatronymicValid: true };

        bool isContactInfoValid =
            _registerContactInfoPageViewModel is { IsEmailValid: true, IsPhoneNumberValid: true };

        bool isCredentialsValid =
            _registerCredentialsPageViewModel is { IsLoginValid: true, IsPasswordValid: true, IsConfirmPasswordValid: true };
        
        return isNameValid && isContactInfoValid && isCredentialsValid;
    }
}