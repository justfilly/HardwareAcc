using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.MVVM.ViewModels.Forms.Base;

namespace HardwareAcc.Services.Navigation;

public interface INavigationService
{
    void Navigate<TViewModel>() where TViewModel : BaseViewModel;
    Page GetPage<TViewModel>() where TViewModel : BaseViewModel;

    void NavigateToForm<TFormViewModel, TModel>(TModel model)
        where TModel : class
        where TFormViewModel : BaseFormViewModel<TModel>;
}