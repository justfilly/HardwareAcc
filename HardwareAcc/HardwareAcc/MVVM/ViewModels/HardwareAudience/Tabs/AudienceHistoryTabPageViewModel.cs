using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Repositories.HardwareAudienceHistory;

namespace HardwareAcc.MVVM.ViewModels.HardwareAudience.Tabs;

public class AudienceHistoryTabPageViewModel : BaseFormViewModel<HardwareModel>
{
     private readonly IHardwareAudienceHistoryRepository _repository;

    public AudienceHistoryTabPageViewModel(IHardwareAudienceHistoryRepository repository)
    {
        _repository = repository;
        _history = new ObservableCollection<HardwareAudienceHistoryModel>();
        DeleteRecordCommand = new RelayCommandWithParameter(DeleteRecord, CanDeleteRecord);
    }
    
    private ObservableCollection<HardwareAudienceHistoryModel> _history;

    public ObservableCollection<HardwareAudienceHistoryModel> History
    {
        get => _history;
    
        set
        {
            _history = value;
            OnPropertyChanged(nameof(History));
        }
    }
    
    private string _searchText = "";
    public string SearchText
    {
        get => _searchText;
    
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
            _ = LoadRecordsAsync();
        }
    }

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
    
    public RelayCommandWithParameter DeleteRecordCommand { get; }

    public override async void Initialize(HardwareModel model)
    {
        base.Initialize(model);
        
        HardwareName = model.Name;
        HardwareInventoryNumber = model.InventoryNumber;
        HardwarePrice = model.Price.ToString();
        
        HardwareStatus =  model.StatusName;
        HardwareResponsibleUser = model.ResponsibleUserLogin;

        HardwareAudience = model.AudienceCode;
        
        await LoadRecordsAsync();
    }

    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.Changed += OnChanged;
    }

    private async void OnChanged() => 
        await LoadRecordsAsync();

    private async Task LoadRecordsAsync()
    {
        IEnumerable<HardwareAudienceHistoryModel> history = await _repository.GetAllByHardwareIdAsync(_model.Id);
        history = history.Reverse();
        
        if (string.IsNullOrEmpty(SearchText) == false)
        {
            history = history.Where(model => model.AudienceCode != null && 
                                             model.AudienceCode.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }
        
        History = new ObservableCollection<HardwareAudienceHistoryModel>(history);
    }
    
    private void DeleteRecord(object model)
    {
        if (model is HardwareAudienceHistoryModel historyModel)
            _repository.DeleteAsync(historyModel.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(HardwareAudienceHistoryModel)}");
    }

    private bool CanDeleteRecord() => 
        true;


    public void Dispose() => 
        _repository.Changed -= OnChanged;
}