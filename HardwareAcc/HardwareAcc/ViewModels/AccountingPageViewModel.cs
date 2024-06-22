using HardwareAcc.Commands;
using HardwareAcc.Services.Navigation;
using HardwareAcc.ViewModels.LoginRegister;

namespace HardwareAcc.ViewModels;

public class AccountingPageViewModel : BaseViewModel
{
    public AccountingPageViewModel(INavigationService navigationService)
    {
        RegisterContactInfoNavigateCommand = new NavigateCommand<LoginPageViewModel>(navigationService);
    }


    public NavigateCommand<LoginPageViewModel> RegisterContactInfoNavigateCommand { get; }
}