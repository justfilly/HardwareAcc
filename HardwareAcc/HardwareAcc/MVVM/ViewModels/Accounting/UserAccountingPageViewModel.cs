using System.Windows.Controls;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Accounting.UserTabs;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.MVVM.ViewModels.Profile;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.Accounting;

public class UserAccountingPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IAuthService _authService;

    public UserAccountingPageViewModel(INavigationService navigationService, IAuthService authService)
    {
        _navigationService = navigationService;
        _authService = authService;

        ProfileNavigateCommand = new NavigateToFormCommand<ProfilePageViewModel, UserModel>(_navigationService);

        HardwareTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsHardwareTabActive = true;
            SwitchTab<UserHardwareTabPageViewModel>();
        });

        SwitchTab<UserHardwareTabPageViewModel>();
        IsHardwareTabActive = true;
    }
    
    public NavigateToFormCommand<ProfilePageViewModel, UserModel> ProfileNavigateCommand { get; }
    public UserModel CurrentUser => _authService.AuthenticatedUser;
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