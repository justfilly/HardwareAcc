using System.Collections.ObjectModel;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.HardwareResponsibility.Tabs;

public class ResponsibilityManageTabPageViewModel : BaseFormViewModel<HardwareModel>
{
    public ResponsibilityManageTabPageViewModel(INavigationService navigationService)
    {
        NavigateToCommentFormCommand = new NavigateToFormCommand<CommentFormPageViewModel, HardwareResponsibilityHistoryModel>(navigationService);
    }

    public NavigateToFormCommand<CommentFormPageViewModel, HardwareResponsibilityHistoryModel> NavigateToCommentFormCommand { get; }
    public RelayCommand TransferResponsibilityCommand { get; }

    #region Props
    
    private string _hardwareName = "";
    public string HardwareName
    {
        get => _hardwareName;
    
        set
        {
            _hardwareName = value;
            OnPropertyChanged(nameof(HardwareName));
        }
    }
    
    private string _hardwareInventoryNumber = "";
    public string HardwareInventoryNumber
    {
        get => _hardwareInventoryNumber;
    
        set
        {
            _hardwareInventoryNumber = value;
            OnPropertyChanged(nameof(HardwareInventoryNumber));
        }
    }
    
    private string _hardwarePrice = "";
    public string HardwarePrice
    {
        get => _hardwarePrice;
    
        set
        {
            _hardwarePrice = value;
            OnPropertyChanged(nameof(HardwarePrice));
        }
    }
    
    private string _hardwareStatus = "";
    public string HardwareStatus
    {
        get => _hardwareStatus;
    
        set
        {
            _hardwareStatus = value;
            OnPropertyChanged(nameof(HardwareStatus));
        }
    }
    
    private string _hardwareResponsibleUser = "";
    public string HardwareResponsibleUser
    {
        get => _hardwareResponsibleUser;
    
        set
        {
            _hardwareResponsibleUser = value;
            OnPropertyChanged(nameof(HardwareResponsibleUser));
        }
    }
    
    private string _hardwareAudience = "";
    public string HardwareAudience
    {
        get => _hardwareAudience;
    
        set
        {
            _hardwareAudience = value;
            OnPropertyChanged(nameof(HardwareAudience));
        }
    }
    
    private ObservableCollection<string> _userCommentItems;
    public ObservableCollection<string> UserCommentItems
    {
        get => _userCommentItems;
    
        set
        {
            _userCommentItems = value;
            OnPropertyChanged(nameof(UserCommentItems));
        }
    }
    
    private string _userCommentSelectedItem = "";
    public string UserCommentSelectedItem
    {
        get => _userCommentSelectedItem;
    
        set
        {
            _userCommentSelectedItem = value;
            OnPropertyChanged(nameof(UserCommentSelectedItem));
        }
    }
    
    private string _userCommentErrorText = "";
    public string UserCommentErrorText
    {
        get => _userCommentErrorText;
    
        set
        {
            _userCommentErrorText = value;
            OnPropertyChanged(nameof(UserCommentErrorText));
        }
    }
    
    private ObservableCollection<string> _userResponsibilityItems;
    public ObservableCollection<string> UserResponsibilityItems
    {
        get => _userResponsibilityItems;
    
        set
        {
            _userResponsibilityItems = value;
            OnPropertyChanged(nameof(UserResponsibilityItems));
        }
    }
    
    private string _userResponsibilitySelectedItem = "";
    public string UserResponsibilitySelectedItem
    {
        get => _userResponsibilitySelectedItem;
    
        set
        {
            _userResponsibilitySelectedItem = value;
            OnPropertyChanged(nameof(UserResponsibilitySelectedItem));
        }
    }
    
    private string _userResponsibilityErrorText = "";
    public string UserResponsibilityErrorText
    {
        get => _userResponsibilityErrorText;
    
        set
        {
            _userResponsibilityErrorText = value;
            OnPropertyChanged(nameof(UserResponsibilityErrorText));
        }
    }
    
    #endregion
    
    public override void Initialize(HardwareModel model)
    {
        base.Initialize(model);
        
        HardwareName = model.Name;
        HardwareInventoryNumber = model.InventoryNumber;
        HardwarePrice = model.Price.ToString();
        /*HardwareStatus = model.Status;
        HardwareResponsibleUser = model.ResponsibleUser;
        HardwareAudience = model.Audience;
        UserCommentItems = new ObservableCollection<string>(model.CommentItems);
        UserResponsibilityItems = new ObservableCollection<string>(model.ResponsibilityItems);*/
    }
}