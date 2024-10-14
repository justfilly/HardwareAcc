using System.Windows.Controls;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Accounting.Tabs;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.MVVM.ViewModels.HardwareResponsibility.Tabs;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.HardwareResponsibility;

public class HardwareResponsibilityPageViewModel : BaseFormViewModel<HardwareModel>
{
    private readonly INavigationService _navigationService;
    
    public HardwareResponsibilityPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        ManageTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsManageTabActive = true;
            SwitchTab<ResponsibilityManageTabPageViewModel>();
        });
        
        HistoryTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsHistoryTabActive = true;
            SwitchTab<ResponsibilityHistoryTabPageViewModel>();
        });

        SwitchTab<ResponsibilityManageTabPageViewModel>();
        IsHistoryTabActive = true;
    }
    
    public RelayCommand HistoryTabCommand { get; }
    public RelayCommand ManageTabCommand { get; }
    
    private Page _tabPage;
    
    public Page TabPage
    {
        get => _tabPage;
    
        set
        {
            _tabPage = value;
            OnPropertyChanged(nameof(TabPage));
        }
    }

    private bool _isManageTabActive;
    public bool IsManageTabActive
    {
        get => _isManageTabActive;
    
        set
        {
            _isManageTabActive = value;
            OnPropertyChanged(nameof(IsManageTabActive));
        }
    }
    
    private bool _isHistoryTabActive;
    public bool IsHistoryTabActive
    {
        get => _isHistoryTabActive;
    
        set
        {
            _isHistoryTabActive = value;
            OnPropertyChanged(nameof(IsHistoryTabActive));
        }
    }

    private void SwitchTab<TViewModel>() where TViewModel : BaseViewModel
    {
        Page view = _navigationService.GetPage<TViewModel>();
        TabPage = view;
    }

    private void ClearActiveTab()
    {
        IsHistoryTabActive = false;
        IsManageTabActive = false;
    }
}