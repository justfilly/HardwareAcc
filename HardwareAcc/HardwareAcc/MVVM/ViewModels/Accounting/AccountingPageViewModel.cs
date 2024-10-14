using System.Windows.Controls;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.ViewModels.Accounting.Tabs;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.Accounting;

public class AccountingPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    
    public AccountingPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        HardwareTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsHardwareTabActive = true;
            SwitchTab<HardwareTabPageViewModel>();
        });
        
        UsersTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsUsersTabActive = true;
            SwitchTab<UsersTabPageViewModel>();
        });
        
        AudiencesTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsAudiencesTabActive = true;
            SwitchTab<AudiencesTabPageViewModel>();
        });
        
        StatusesTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsStatusesTabActive = true;
            SwitchTab<StatusesTabPageViewModel>();
        });
        
        SwitchTab<HardwareTabPageViewModel>();
        IsHardwareTabActive = true;
    }
    
    public RelayCommand HardwareTabCommand { get; }
    public RelayCommand UsersTabCommand { get; }
    public RelayCommand AudiencesTabCommand { get; }
    public RelayCommand StatusesTabCommand { get; }
    
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
    
    private bool _isUsersTabActive;
    public bool IsUsersTabActive
    {
        get => _isUsersTabActive;
    
        set
        {
            _isUsersTabActive = value;
            OnPropertyChanged(nameof(IsUsersTabActive));
        }
    }
    
    private bool _isAudiencesTabActive;
    public bool IsAudiencesTabActive
    {
        get => _isAudiencesTabActive;
    
        set
        {
            _isAudiencesTabActive = value;
            OnPropertyChanged(nameof(IsAudiencesTabActive));
        }
    }
    
    private bool _isStatusesTabActive;
    public bool IsStatusesTabActive
    {
        get => _isStatusesTabActive;
    
        set
        {
            _isStatusesTabActive = value;
            OnPropertyChanged(nameof(IsStatusesTabActive));
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
        IsUsersTabActive = false;
        IsAudiencesTabActive = false;
        IsStatusesTabActive = false;
    }
}