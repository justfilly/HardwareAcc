using System.Collections.ObjectModel;
using HardwareAcc.Models;

namespace HardwareAcc.ViewModels.Tabs;

public class AudiencesTabPageViewModel : BaseViewModel
{
    public AudiencesTabPageViewModel()
    {
        _audiences = new ObservableCollection<Audience>()
        {
            new() { Id = 1, Name = "Computer Science Class", Code = "A423" },
            new() { Id = 2, Name = "Chemistry lab", Code = "A107" },
            new() { Id = 3, Name = "Web design class", Code = "A210" },
        };
    }
    
    private ObservableCollection<Audience> _audiences;
    public ObservableCollection<Audience> Audiences
    {
        get => _audiences;
    
        set
        {
            _audiences = value;
            OnPropertyChanged(nameof(Audiences));
        }
    }
}