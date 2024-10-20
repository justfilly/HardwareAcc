using HardwareAcc.MVVM.ViewModels;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.Commands;

public class NavigateCommand<TViewModel> : BaseCommand where TViewModel : BaseViewModel
 {
     private readonly INavigationService _navigationService;
 
     public NavigateCommand(INavigationService navigationService)
     {
         _navigationService = navigationService;
     }
 
     public override void Execute(object parameter) => 
         _navigationService.Navigate<TViewModel>();
 }
 
 
public class NavigateToFormCommand<TFormViewModel, TModel> : BaseCommand 
    where TModel : class
    where TFormViewModel : BaseFormViewModel<TModel>
{
    private readonly INavigationService _navigationService;

    public NavigateToFormCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object parameter) =>
        _navigationService.NavigateToForm<TFormViewModel, TModel>((parameter as TModel)!);
}