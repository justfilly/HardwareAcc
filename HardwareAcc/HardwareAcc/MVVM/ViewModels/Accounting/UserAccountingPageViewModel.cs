using System.Windows.Controls;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.ViewModels.Accounting.Tabs;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.Accounting;

public class UserAccountingPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    
    public UserAccountingPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        HardwareTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsHardwareTabActive = true;
            SwitchTab<HardwareTabPageViewModel>();
        });

        SwitchTab<HardwareTabPageViewModel>();
        IsHardwareTabActive = true;
    }
    
    public RelayCommand HardwareTabCommand { get; }

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

    private bool _isHardwareTabActive;
    public bool IsHardwareTabActive
    {
        get => _isHardwareTabActive;
    
        set
        {
            _isHardwareTabActive = value;
            OnPropertyChanged(nameof(IsHardwareTabActive));
        }
    }
    
    private void SwitchTab<TViewModel>() where TViewModel : BaseViewModel
    {
        Page view = _navigationService.GetPage<TViewModel>();
        TabPage = view;
    }

    private void ClearActiveTab()
    {
        IsHardwareTabActive = false;
    }
}