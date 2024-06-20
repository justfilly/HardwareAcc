using System;
using System.Windows.Controls;
using HardwareAcc.Services.ViewLocator;
using HardwareAcc.ViewModels;
using HardwareAcc.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareAcc.Services.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewLocator _viewLocator;
    private readonly MainWindowView _mainWindowView;

    public NavigationService(IServiceProvider serviceProvider, IViewLocator viewLocator, MainWindowView mainWindowView)
    {
        _serviceProvider = serviceProvider;
        _viewLocator = viewLocator;
        _mainWindowView = mainWindowView;
    }

    public void Navigate<TViewModel>() where TViewModel : BaseViewModel
    {
        TViewModel viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        
        Type viewType = _viewLocator.GetViewType(viewModel.GetType());
        Page view = (Page)_serviceProvider.GetRequiredService(viewType);
        view.DataContext = viewModel;
        
        _mainWindowView.MainFrame.Content = view;
    }
}