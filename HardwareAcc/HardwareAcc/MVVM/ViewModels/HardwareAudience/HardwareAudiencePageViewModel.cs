using System.Windows.Controls;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Accounting;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.MVVM.ViewModels.HardwareResponsibility.Tabs;
using HardwareAcc.Services.FormsProvider;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.HardwareResponsibility;

public class HardwareAudiencePageViewModel : BaseFormViewModel<HardwareModel>
{
    private readonly INavigationService _navigationService;
    private readonly IFormsProvider _formsProvider;

    public HardwareAudiencePageViewModel(INavigationService navigationService, IFormsProvider formsProvider)
    {
        _navigationService = navigationService;
        _formsProvider = formsProvider;
        AccountingNavigateCommand = new NavigateCommand<AccountingPageViewModel>(navigationService);

        ManageTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsManageTabActive = true;
            SwitchTab<ResponsibilityManageTabPageViewModel, HardwareModel>(_model);
        });
        
        HistoryTabCommand = new RelayCommand(() =>
        {
            ClearActiveTab();
            IsHistoryTabActive = true;
            SwitchTab<ResponsibilityHistoryTabPageViewModel, HardwareModel>(_model);
        });
    }
    
    public NavigateCommand<AccountingPageViewModel> AccountingNavigateCommand { get; }
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

    public override void Initialize(HardwareModel model)
    {
        base.Initialize(model);
        
        ClearActiveTab();
        IsManageTabActive = true;
        SwitchTab<ResponsibilityManageTabPageViewModel, HardwareModel>(_model);
    }

    private void ClearActiveTab()
    {
        IsHistoryTabActive = false;
        IsManageTabActive = false;
    }

    private void SwitchTab<TFormViewModel, TModel>(TModel model) 
        where TFormViewModel : BaseFormViewModel<TModel>
        where TModel : class
    {
        Page view = _navigationService.GetPage<TFormViewModel>();
        TFormViewModel viewModel = _formsProvider.GetFormViewModel<TFormViewModel>();
        viewModel.Initialize(model);
        TabPage = view;
    }
}