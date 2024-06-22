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
        bool isNameValid = 
            _registerNamePageViewModel is { IsFirstNameValid: true, IsSecondNameValid: true, IsPatronymicValid: true };
        
        bool isContactInfoValid = 
            _registerContactInfoPageViewModel is { IsEmailValid: true, IsPhoneNumberValid: true };
        
        bool isCredentialsValid = 
            _registerCredentialsPageViewModel is { IsLoginValid: true, IsPasswordValid: true, IsConfirmPasswordValid: true };
        
        if (isNameValid && isContactInfoValid && isCredentialsValid)
        {
            User user = new User
            {
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