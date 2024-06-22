using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels.LoginRegister;
using HardwareAcc.Views.LoginRegister;

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
                User user = new User
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
       
                await _authService.RegisterAsync(user);
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