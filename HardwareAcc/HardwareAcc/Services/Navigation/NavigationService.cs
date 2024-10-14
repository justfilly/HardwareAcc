using System;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.MVVM.Views;
using HardwareAcc.Services.FormsProvider;
using HardwareAcc.Services.ViewLocator;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareAcc.Services.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewLocator _viewLocator;
    private readonly MainWindowView _mainWindowView;
    private readonly IFormsProvider _formsProvider;

    public NavigationService(IServiceProvider serviceProvider,
        IViewLocator viewLocator,
        MainWindowView mainWindowView,
        IFormsProvider formsProvider)
    {
        _serviceProvider = serviceProvider;
        _viewLocator = viewLocator;
        _mainWindowView = mainWindowView;
        _formsProvider = formsProvider;
    }

    public void Navigate<TViewModel>() where TViewModel : BaseViewModel
    {
        Page view = GetPage<TViewModel>();
        _mainWindowView.MainFrame.Content = view;
    }

    public void NavigateToForm<TFormViewModel, TModel>(TModel model)
        where TModel : class
        where TFormViewModel : BaseFormViewModel<TModel>
    {
        TFormViewModel viewModel = _formsProvider.GetFormViewModel<TFormViewModel>();
        viewModel.Initialize(model);
        
        Navigate<TFormViewModel>();
    }

    public Page GetPage<TViewModel>() where TViewModel : BaseViewModel
    {
        TViewModel viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        
        Type viewType = _viewLocator.GetViewType(viewModel.GetType());
        Page view = (Page)_serviceProvider.GetRequiredService(viewType);
        view.DataContext = viewModel;
        
        return view;
    }
}