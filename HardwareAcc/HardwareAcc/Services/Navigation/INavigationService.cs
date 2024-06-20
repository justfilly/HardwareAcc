using HardwareAcc.ViewModels;

namespace HardwareAcc.Services.Navigation;

public interface INavigationService
{
    void Navigate<TViewModel>() where TViewModel : BaseViewModel;
}