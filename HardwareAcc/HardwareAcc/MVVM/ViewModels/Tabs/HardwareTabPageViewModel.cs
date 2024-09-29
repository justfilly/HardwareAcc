using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Hardware;

namespace HardwareAcc.MVVM.ViewModels.Tabs;

public class HardwareTabPageViewModel : BaseViewModel, IDisposable
{
    private readonly IHardwareRepository _repository;

    public HardwareTabPageViewModel(IHardwareRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _hardware = new ObservableCollection<HardwareModel>();
        FormNavigateCommand = new NavigateToFormCommand<HardwareFormPageViewModel, HardwareModel>(navigationService);
        DeleteRecordCommand = new RelayCommandWithParameter(DeleteRecord, CanDeleteRecord);
    }

    private ObservableCollection<HardwareModel> _hardware;

    public ObservableCollection<HardwareModel> Hardware
    {
        get => _hardware;
    
        set
        {
            _hardware = value;
            OnPropertyChanged(nameof(Hardware));
        }
    }
    
    public NavigateToFormCommand<HardwareFormPageViewModel, HardwareModel> FormNavigateCommand { get; }
    public RelayCommandWithParameter DeleteRecordCommand { get; }
    public static HardwareModel NewModel => new();
    
    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.Changed += OnHardwareChanged;
    }

    private async void OnHardwareChanged() => 
        await LoadRecordsAsync();
    
    private async Task LoadRecordsAsync()
    {
        IEnumerable<HardwareModel> hardware = await _repository.GetAllAsync();
        Hardware = new ObservableCollection<HardwareModel>(hardware);
    }
    
    private void DeleteRecord(object model)
    {
        if (model is HardwareModel hardware)
            _repository.DeleteAsync(hardware.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(HardwareModel)}");
    }

    private static bool CanDeleteRecord() => 
        true;


    public void Dispose() => 
        _repository.Changed -= OnHardwareChanged;
}