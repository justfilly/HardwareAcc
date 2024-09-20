using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.Models;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.ViewModels.Forms;

namespace HardwareAcc.ViewModels.Tabs;

public class AudiencesTabPageViewModel : BaseViewModel, IDisposable
{
    private readonly IAudienceRepository _audienceRepository;

    public AudiencesTabPageViewModel(IAudienceRepository audienceRepository, INavigationService navigationService)
    {
        _audienceRepository = audienceRepository;
        _audiences = new ObservableCollection<AudienceModel>();
        AudiencesFormNavigateCommand = new NavigateToFormCommand<AudiencesFormPageViewModel, AudienceModel>(navigationService);
        DeleteAudienceCommand = new RelayCommandWithParameter(DeleteAudience, CanDeleteAudience);
        
        _audienceRepository.AudiencesChanged += OnAudiencesChanged;
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

    public NavigateToFormCommand<AudiencesFormPageViewModel, AudienceModel> AudiencesFormNavigateCommand { get; }
    public RelayCommandWithParameter DeleteAudienceCommand { get; }
    public static AudienceModel NewAudienceModel => new();

    public async Task InitializeAsync() => 
        await LoadAudiencesAsync();

    private async void OnAudiencesChanged() => 
        await LoadAudiencesAsync();

    private async Task LoadAudiencesAsync()
    {
        var audiences = await _audienceRepository.GetAllAudiencesAsync();
        Audiences = new ObservableCollection<AudienceModel>(audiences);
    }
    
    private void DeleteAudience(object model)
    {
        if (model is AudienceModel audience)
            _audienceRepository.DeleteAudienceAsync(audience.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(AudienceModel)}");
    }

    private bool CanDeleteAudience() => 
        true;


    public void Dispose() => 
        _audienceRepository.AudiencesChanged -= OnAudiencesChanged;
}