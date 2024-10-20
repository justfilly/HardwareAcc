using System.Collections.Generic;
using HardwareAcc.MVVM.ViewModels;

namespace HardwareAcc.Services.FormsProvider;

public interface IFormsProvider
{
    TViewModel GetFormViewModel<TViewModel>() where TViewModel : class;
    void Initialize(IEnumerable<BaseViewModel> formViewModels);
}