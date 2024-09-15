using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.Repositories.Audience;

namespace HardwareAcc.ViewModels.Tabs;

public class AudiencesTabPageViewModel : BaseViewModel
{
    private readonly IAudienceRepository _audienceRepository;

    public AudiencesTabPageViewModel(IAudienceRepository audienceRepository)
    {
        _audienceRepository = audienceRepository;
        _audiences = new ObservableCollection<AudienceModel>();
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

    public async Task InitializeAsync() => 
        await LoadAudiencesAsync();

    private async Task LoadAudiencesAsync()
    {
        var audiences = await _audienceRepository.GetAllAudiencesAsync();
        Audiences = new ObservableCollection<AudienceModel>(audiences);
    }
}