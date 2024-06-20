using System;
using System.Collections.Generic;
using System.Windows.Controls;
using HardwareAcc.ViewModels;

namespace HardwareAcc.Services.ViewLocator;

public class ViewLocator : IViewLocator
{
    private readonly Dictionary<Type, Type> _viewModelToViewMap = new();
    
    public void Register<TViewModel, TView>() 
        where TViewModel : BaseViewModel
        where TView : Page
    {
        _viewModelToViewMap[typeof(TViewModel)] = typeof(TView);
    }

    public Type GetViewType(Type viewModelType)
    {
        return _viewModelToViewMap[viewModelType];
    }
}