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

    private string _initialCode = "";
    
    public AudiencesFormPageViewModel(IAudienceRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _navigationService = navigationService;
        
        AccountingNavigateCommand = new NavigateCommand<AccountingPageViewModel>(navigationService);
        SubmitCommand = new RelayCommand(Submit, CanSubmit);
    }
    
    public NavigateCommand<AccountingPageViewModel> AccountingNavigateCommand { get; }
    public RelayCommand SubmitCommand { get; }

    #region FieldProperties
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

    private string _codeErrorText = "";

    public string CodeErrorText
    {
        get => _codeErrorText;
    
        set
        {
            _codeErrorText = value;
            OnPropertyChanged(nameof(CodeErrorText));
        }
    }
    #endregion
    
    public override void Initialize(AudienceModel model)
    {
        base.Initialize(model);

        int? id = model?.Id;

        Name = "";
        Code = "";

        if (id == 0) {
            _mode = FormMode.Add;

            IsNameValid = true;

            IsCodeValid = false;
            _initialCode = "";
        }
        else {
            _initialCode = model?.Code!;
            _mode = FormMode.Edit;
            
            Name = model?.Name!;
            Code = model?.Code!;
            
            _isNameValid = true;
            _isCodeValid = true;
        }
    }

    private async void Submit()
    {
        if (await IsCodeUnique() == false)
            return;
        
        _model.Name = Name;
        _model.Code = Code;
        
        if (_mode == FormMode.Add)
            await _repository.AddAsync(_model);
        else
            await _repository.UpdateAsync(_model);

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
        
        if (await _repository.GetByCodeAsync(Code) != null) {
            CodeErrorText = "Code is not unique";
            return false;
        }

        return true;
    }
}