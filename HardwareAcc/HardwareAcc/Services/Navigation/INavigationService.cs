using System.Windows.Controls;
using HardwareAcc.ViewModels;

namespace HardwareAcc.Services.Navigation;

public interface INavigationService
{
    void Navigate<TViewModel>() where TViewModel : BaseViewModel;
    Page GetPage<TViewModel>() where TViewModel : BaseViewModel;
}