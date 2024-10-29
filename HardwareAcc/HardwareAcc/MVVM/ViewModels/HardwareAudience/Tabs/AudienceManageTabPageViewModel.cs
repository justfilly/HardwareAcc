using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.Services.Repositories.Hardware;
using HardwareAcc.Services.Repositories.HardwareAudienceHistory;
using HardwareAcc.Services.Repositories.HardwareResponsibilityHistory;

namespace HardwareAcc.MVVM.ViewModels.HardwareAudience.Tabs;

public class AudienceManageTabPageViewModel : BaseFormViewModel<HardwareModel>
{
    private readonly IHardwareAudienceHistoryRepository _hardwareAudienceHistoryRepository;
    private readonly IAudienceRepository _audienceRepository;
    private readonly IHardwareRepository _hardwareRepository;

    public AudienceManageTabPageViewModel(IHardwareAudienceHistoryRepository hardwareAudienceHistoryRepository,
        IAudienceRepository audienceRepository,
        IHardwareRepository hardwareRepository)
    {
        _hardwareAudienceHistoryRepository = hardwareAudienceHistoryRepository;
        _audienceRepository = audienceRepository;
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
    
        IEnumerable<AudienceModel> models = 
            await _audienceRepository.GetAllAsync();
    
        foreach (AudienceModel audienceModel in models)
        {
            AudiencesItems.Add(audienceModel.Code);
        }
    }

    private async void ChangeAudience()
    {
        AudienceModel audienceModel = await _audienceRepository.GetByCodeAsync(AudiencesSelectedItem);

        HardwareAudienceHistoryModel historyModel = new()
        {
            HardwareId = _model.Id,
            AudienceId = audienceModel.Id,
            TransferredDate = DateTime.Now,
            AudienceCode = audienceModel.Code,
        };

        _model.AudienceId = audienceModel.Id;
        _model.AudienceCode = audienceModel.Code;

        await _hardwareAudienceHistoryRepository.AddAsync(historyModel);
        await _hardwareRepository.UpdateAsync(_model);  
        
        HardwareAudience = audienceModel.Code;
    }
}