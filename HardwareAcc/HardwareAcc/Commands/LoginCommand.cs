using System;
using HardwareAcc.Services.AuthService;
using HardwareAcc.ViewModels.LoginRegister.Pages;

namespace HardwareAcc.Commands;

public class LoginCommand : BaseCommand
{
    private readonly LoginPageViewModel _loginPageViewModel;
    private readonly IAuthService _authService;

    public LoginCommand(LoginPageViewModel loginPageViewModel, IAuthService authService)
    {
        _loginPageViewModel = loginPageViewModel;
        _authService = authService;
    }

    public override void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }
}