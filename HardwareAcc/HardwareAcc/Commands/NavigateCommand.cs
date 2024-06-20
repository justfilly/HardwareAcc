using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels;

namespace HardwareAcc.Commands;

public class NavigateCommand<TViewModel> : BaseCommand where TViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public NavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter) => 
        _navigationService.Navigate<TViewModel>();
}