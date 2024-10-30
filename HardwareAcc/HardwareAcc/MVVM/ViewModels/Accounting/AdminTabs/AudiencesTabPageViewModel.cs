using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Audience;

namespace HardwareAcc.MVVM.ViewModels.Accounting.AdminTabs;

public class AudiencesTabPageViewModel : BaseViewModel, IDisposable
{
    private readonly IAudienceRepository _repository;

    public AudiencesTabPageViewModel(IAudienceRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _audiences = new ObservableCollection<AudienceModel>();
        FormNavigateCommand = new NavigateToFormCommand<AudiencesFormPageViewModel, AudienceModel>(navigationService);
        DeleteRecordCommand = new RelayCommandWithParameter(DeleteRecord, CanDeleteRecord);
    }
    
    private ObservableCollection<AudienceModel> _audiences;

    public ObservableCollection<AudienceModel> Audiences
    {
        get => _audiences;
    
        set
        {
            _audiences = value;
            OnPropertyChanged(nameof(Audiences));
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

    public NavigateToFormCommand<AudiencesFormPageViewModel, AudienceModel> FormNavigateCommand { get; }
    public RelayCommandWithParameter DeleteRecordCommand { get; }
    public static AudienceModel NewAudienceModel => new();

    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.Changed += OnChanged;
    }

    private async void OnChanged() => 
        await LoadRecordsAsync();

    private async Task LoadRecordsAsync()
    {
        IEnumerable<AudienceModel> audiences = await _repository.GetAllAsync();
        
        if (string.IsNullOrEmpty(SearchText) == false)
        {
            audiences = audiences.Where(model => model.Name != null && 
                                                 model.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }
        
        Audiences = new ObservableCollection<AudienceModel>(audiences);
    }
    
    private void DeleteRecord(object model)
    {
        if (model is AudienceModel audience)
            _repository.DeleteAsync(audience.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(AudienceModel)}");
    }

    private bool CanDeleteRecord() => 
        true;


    public void Dispose() => 
        _repository.Changed -= OnChanged;
}