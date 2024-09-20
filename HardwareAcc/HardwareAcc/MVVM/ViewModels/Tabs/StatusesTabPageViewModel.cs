using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Status;

namespace HardwareAcc.MVVM.ViewModels.Tabs;

public class StatusesTabPageViewModel : BaseViewModel
{
    private readonly IStatusRepository _repository;

    public StatusesTabPageViewModel(IStatusRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _statuses = new ObservableCollection<StatusModel>();
        FormNavigateCommand = new NavigateToFormCommand<StatusesFormPageViewModel, StatusModel>(navigationService);
        DeleteRecordCommand = new RelayCommandWithParameter(DeleteRecord, CanDeleteRecord);
        
    }

    private ObservableCollection<StatusModel> _statuses;

    public ObservableCollection<StatusModel> Statuses
    {
        get => _statuses;
    
        set
        {
            _statuses = value;
            OnPropertyChanged(nameof(Statuses));
        }
    }

    public NavigateToFormCommand<StatusesFormPageViewModel, StatusModel> FormNavigateCommand { get; }
    public RelayCommandWithParameter DeleteRecordCommand { get; }
    public static StatusModel NewModel => new();

    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.StatusesChanged += OnStatusesChanged;
    }

    private async void OnStatusesChanged() => 
        await LoadRecordsAsync();

    private async Task LoadRecordsAsync()
    {
        IEnumerable<StatusModel> statuses = await _repository.GetAllStatusesAsync();
        Statuses = new ObservableCollection<StatusModel>(statuses);
    }
    
    private void DeleteRecord(object model)
    {
        if (model is StatusModel status)
            _repository.DeleteStatusAsync(status.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(StatusModel)}");
    }

    private bool CanDeleteRecord() => 
        true;


    public void Dispose() => 
        _repository.StatusesChanged -= OnStatusesChanged;
}