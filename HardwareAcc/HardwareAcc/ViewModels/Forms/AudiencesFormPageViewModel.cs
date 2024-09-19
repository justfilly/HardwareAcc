using HardwareAcc.Commands;
using HardwareAcc.Models;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.ViewModels.Forms;

public class AudiencesFormPageViewModel : BaseFormViewModel<AudienceModel>
{
    private readonly INavigationService _navigationService;

    public AudiencesFormPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        AccountingNavigateCommand = new NavigateCommand<AccountingPageViewModel>(navigationService);
        SubmitCommand = new RelayCommand(Submit, CanSubmit);
    }
    
    public NavigateCommand<AccountingPageViewModel> AccountingNavigateCommand { get; }
    public RelayCommand SubmitCommand { get; }
    
    private string _name = "";
    public string Name
    {
        get => _name;
    
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    
    private bool _isNameValid;
    public bool IsNameValid
    {
        get => _isNameValid;
    
        set
        {
            _isNameValid = value;
            OnPropertyChanged(nameof(IsNameValid));
        }
    }
    
    private string _code = "";
    public string Code
    {
        get => _code;
    
        set
        {
            _code = value;
            OnPropertyChanged(nameof(Code));
        }
    }
    
    private bool _isCodeValid;
    public bool IsCodeValid
    {
        get => _isCodeValid;
    
        set
        {
            _isCodeValid = value;
            OnPropertyChanged(nameof(IsCodeValid));
        }
    }

    public override void SetModel(AudienceModel? model)
    {
        base.SetModel(model);

        Name = model?.Name!;
        Code = model?.Code!;
    }

    private void Submit()
    {
        _navigationService.Navigate<AccountingPageViewModel>();
    }

    private bool CanSubmit()
    {
        return IsNameValid && IsCodeValid;
    }
}