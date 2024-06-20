using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels.Pages;

namespace HardwareAcc.Commands;

public class RegisterNavigateCommand : BaseCommand
{
    private readonly INavigationService _navigationService;

    public RegisterNavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate<RegisterNamePageViewModel>();
    }
}