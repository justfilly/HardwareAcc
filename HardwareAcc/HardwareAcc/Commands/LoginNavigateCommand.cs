using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels.Pages;

namespace HardwareAcc.Commands;

public class LoginNavigateCommand : BaseCommand
{
    private readonly INavigationService _navigationService;

    public LoginNavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate<LoginPageViewModel>();
    }
}