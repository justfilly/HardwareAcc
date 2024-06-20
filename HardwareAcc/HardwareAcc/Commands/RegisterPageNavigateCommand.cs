using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels.Pages;

namespace HardwareAcc.Commands;

public class RegisterPageNavigateCommand : BaseCommand
{
    private readonly INavigationService _navigationService;

    public RegisterPageNavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate<RegisterPageViewModel>();
    }
}