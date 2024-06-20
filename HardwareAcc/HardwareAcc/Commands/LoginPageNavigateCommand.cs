using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels.Pages;

namespace HardwareAcc.Commands;

public class LoginPageNavigateCommand : BaseCommand
{
    private readonly INavigationService _navigationService;

    public LoginPageNavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate<LoginPageViewModel>();
    }
}