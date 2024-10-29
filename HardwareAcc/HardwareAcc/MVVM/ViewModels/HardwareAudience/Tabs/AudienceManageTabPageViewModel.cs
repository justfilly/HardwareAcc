using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.Services.Repositories.Hardware;
using HardwareAcc.Services.Repositories.HardwareResponsibilityHistory;
using HardwareAcc.Services.Repositories.Status;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.HardwareAudience.Tabs;

public class AudienceManageTabPageViewModel : BaseFormViewModel<HardwareModel>
{
    private readonly INavigationService _navigationService;
    private readonly IStatusRepository _statusRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAudienceRepository _audienceRepository;
    private readonly IHardwareRepository _hardwareRepository;
    private readonly IHardwareResponsibilityHistoryRepository _hardwareResponsibilityHistoryRepository;
    
    public AudienceManageTabPageViewModel(INavigationService navigationService,
        IStatusRepository statusRepository,
        IUserRepository userRepository,
        IAudienceRepository audienceRepository, 
        IHardwareResponsibilityHistoryRepository hardwareResponsibilityHistoryRepository, 
        IHardwareRepository hardwareRepository)
    {
        _navigationService = navigationService;
        _statusRepository = statusRepository;
        _userRepository = userRepository;
        _audienceRepository = audienceRepository;
        _hardwareResponsibilityHistoryRepository = hardwareResponsibilityHistoryRepository;
        _hardwareRepository = hardwareRepository;

        ChangeAudienceCommand = new RelayCommand(ChangeAudience);
    }

    public RelayCommand ChangeAudienceCommand { get; }

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

    private ObservableCollection<string> _audiencesItems = new();

    public ObservableCollection<string> AudiencesItems
    {
        get => _audiencesItems;
    
        set
        {
            _audiencesItems = value;
            OnPropertyChanged(nameof(AudiencesItems));
        }
    }

    private string _audiencesSelectedItem = "";

    public string AudiencesSelectedItem
    {
        get => _audiencesSelectedItem;
    
        set
        {
            _audiencesSelectedItem = value;
            OnPropertyChanged(nameof(AudiencesSelectedItem));
        }
    }

    private string _audiencesErrorText = "";

    public string AudiencesErrorText
    {
        get => _audiencesErrorText;
    
        set
        {
            _audiencesErrorText = value;
            OnPropertyChanged(nameof(AudiencesErrorText));
        }
    }

    #endregion

    public override async void Initialize(HardwareModel model)
    {
        base.Initialize(model);
        
        HardwareName = model.Name;
        HardwareInventoryNumber = model.InventoryNumber;
        HardwarePrice = model.Price.ToString();
        
        HardwareStatus =  model.StatusName;
        HardwareResponsibleUser = model.ResponsibleUserLogin;

        HardwareAudience = model.AudienceCode;
        
        await ResetAudiencesItems();
    }

    private async Task ResetAudiencesItems()
    {
        AudiencesItems.Clear();
    
        IEnumerable<HardwareResponsibilityHistoryModel> model = 
            await _hardwareResponsibilityHistoryRepository.GetAllByHardwareIdAsync(_model.Id);
    
        var sortedModel = model.OrderByDescending(m => m.ResponsibilityStartDate);

        foreach (HardwareResponsibilityHistoryModel historyModel in sortedModel)
        {
            UserModel userModel = await _userRepository.GetByIdAsync(historyModel.ResponsibleUserId);

            string endDateText = historyModel.ResponsibilityEndDate == DateTime.MinValue 
                ? "Present" 
                : historyModel.ResponsibilityEndDate.ToString("dd/MM/yyyy HH:mm:ss");

            string itemText = $"{userModel.Login}, {historyModel.ResponsibilityStartDate:dd/MM/yyyy HH:mm:ss} - {endDateText}";
            AudiencesItems.Add(itemText);
        }
    }

    private async void ChangeAudience()
    {
    }
}