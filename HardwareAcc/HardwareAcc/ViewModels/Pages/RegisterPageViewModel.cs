using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Pages;

public class RegisterPageViewModel : BaseViewModel
{
    public RegisterPageViewModel(INavigationService navigationService)
    {
        LoginPageNavigateCommand = new LoginPageNavigateCommand(navigationService);
    }

    public LoginPageNavigateCommand LoginPageNavigateCommand { get; }
}