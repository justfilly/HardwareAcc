using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Status;

namespace HardwareAcc.MVVM.ViewModels.Accounting.Tabs;

public class StatusesTabPageViewModel : BaseViewModel, IDisposable
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

    public NavigateToFormCommand<StatusesFormPageViewModel, StatusModel> FormNavigateCommand { get; }
    public RelayCommandWithParameter DeleteRecordCommand { get; }
    public static StatusModel NewModel => new();

    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.Changed += OnChanged;
    }

    private async void OnChanged() => 
        await LoadRecordsAsync();

    private async Task LoadRecordsAsync()
    {
        IEnumerable<StatusModel> statuses = await _repository.GetAllAsync();
        
        if (string.IsNullOrEmpty(SearchText) == false)
            statuses = statuses.Where(model => model.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        
        Statuses = new ObservableCollection<StatusModel>(statuses);
    }
    
    private void DeleteRecord(object model)
    {
        if (model is StatusModel status)
            _repository.DeleteAsync(status.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(StatusModel)}");
    }

    private bool CanDeleteRecord() => 
        true;


    public void Dispose() => 
        _repository.Changed -= OnChanged;
}