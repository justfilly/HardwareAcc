using System;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Audience;

namespace HardwareAcc.MVVM.ViewModels.Forms;

public class AudiencesFormPageViewModel : BaseFormViewModel<AudienceModel>
{
    private readonly IAudienceRepository _repository;
    private readonly INavigationService _navigationService;

    public AudiencesFormPageViewModel(IAudienceRepository repository, INavigationService navigationService)
    {
        _repository = repository;
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
            _model!.Name = value;
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
            _model!.Code = value;
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

    private string _codeErrorText = "";
    private string _initialCode;

    public string CodeErrorText
    {
        get => _codeErrorText;
    
        set
        {
            _codeErrorText = value;
            OnPropertyChanged(nameof(CodeErrorText));
        }
    }
    
    public override void SetModel(AudienceModel? model)
    {
        base.SetModel(model);

        int? id = model?.Id;
        
        if (id == 0)
            _mode = FormMode.Add;
        else 
        {
            _initialCode = model?.Code!;
            _mode = FormMode.Edit;
        }

        Name = model?.Name!;
        Code = model?.Code!;
    }

    private async void Submit()
    {
        switch (_mode) {
            case FormMode.Add:
            {
                if (await IsCodeUnique() == false)
                    return;
                
                await _repository.AddAudienceAsync(_model!);
            }
                break;
            case FormMode.Edit:
            {
                await _repository.UpdateAudienceAsync(_model!);
            }
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unsupported mode: {_mode}");
        }

        _navigationService.Navigate<AccountingPageViewModel>();
    }

    private bool CanSubmit()
    {
        return IsNameValid && IsCodeValid;
    }

    private async Task<bool> IsCodeUnique()
    {
        if (_mode == FormMode.Edit && _initialCode == Code)
            return true;
        
        if (await _repository.GetAudienceByCodeAsync(Code) != null) {
            CodeErrorText = "Code is not unique";
            return false;
        }

        return true;
    }
}