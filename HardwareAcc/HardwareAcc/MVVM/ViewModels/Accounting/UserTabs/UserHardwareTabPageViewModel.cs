using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.Repositories.Hardware;

namespace HardwareAcc.MVVM.ViewModels.Accounting.UserTabs;

public class UserHardwareTabPageViewModel : BaseViewModel, IDisposable
{
    private readonly IHardwareRepository _repository;
    private readonly IAuthService _authService;

    public UserHardwareTabPageViewModel(IHardwareRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
        _hardware = new ObservableCollection<HardwareModel>();
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
    
    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.Changed += OnHardwareChanged;
    }

    private async void OnHardwareChanged() => 
        await LoadRecordsAsync();
    
    private async Task LoadRecordsAsync()
    {
        IEnumerable<HardwareModel> hardware = 
            await _repository.GetAllByResponsibleUserIdAsync(_authService.AuthenticatedUser.Id);

        if (string.IsNullOrEmpty(SearchText) == false)
            hardware = hardware.Where(model => model.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        Hardware = new ObservableCollection<HardwareModel>(hardware);
    }
    
    public void Dispose() => 
        _repository.Changed -= OnHardwareChanged;
}