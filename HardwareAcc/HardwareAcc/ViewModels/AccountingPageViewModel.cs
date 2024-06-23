using System;
using System.Windows.Controls;
using HardwareAcc.Commands;
using HardwareAcc.Services.ViewLocator;
using HardwareAcc.ViewModels.Tabs;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareAcc.ViewModels;

public class AccountingPageViewModel : BaseViewModel
{
    private IServiceProvider _serviceProvider;
    private IViewLocator _viewLocator;
    
    public AccountingPageViewModel(IServiceProvider serviceProvider, IViewLocator viewLocator)
    {
        _serviceProvider = serviceProvider;
        _viewLocator = viewLocator;
        
        HardwareTabCommand = new RelayCommand(SwitchTab<HardwareTabPageViewModel>);
        UsersTabCommand = new RelayCommand(SwitchTab<UsersTabPageViewModel>);
        AudiencesTabCommand = new RelayCommand(SwitchTab<AudiencesTabPageViewModel>);
        StatusesTabCommand = new RelayCommand(SwitchTab<StatusesTabPageViewModel>);
        
        SwitchTab<HardwareTabPageViewModel>();
    }
    
    public RelayCommand HardwareTabCommand { get; }
    public RelayCommand UsersTabCommand { get; }
    public RelayCommand AudiencesTabCommand { get; }
    public RelayCommand StatusesTabCommand { get; }
    
    private Page? _tabPage;
    
    public Page? TabPage
    {
        get => _tabPage;
    
        set
        {
            _tabPage = value;
            OnPropertyChanged(nameof(TabPage));
        }
    }

    private void SwitchTab<TViewModel>() where TViewModel : BaseViewModel
    {
        TViewModel viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        
        Type viewType = _viewLocator.GetViewType(viewModel.GetType());
        Page view = (Page)_serviceProvider.GetRequiredService(viewType);
        view.DataContext = viewModel;
        
        TabPage = view;
    }
}