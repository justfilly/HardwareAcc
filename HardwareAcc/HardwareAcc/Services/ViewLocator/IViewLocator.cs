using System;
using System.Windows.Controls;
using HardwareAcc.ViewModels;

namespace HardwareAcc.Services.ViewLocator;

public interface IViewLocator
{
    void Register<TViewModel, TView>() 
        where TViewModel : BaseViewModel
        where TView : Page;

    Type GetViewType(Type viewModelType);
}