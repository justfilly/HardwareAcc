using System;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Status;

namespace HardwareAcc.MVVM.ViewModels.Forms;

public class StatusesFormPageViewModel : BaseFormViewModel<StatusModel>
{
     private readonly IStatusRepository _repository;
    private readonly INavigationService _navigationService;

    private string _initialName = "";
    
    public StatusesFormPageViewModel(IStatusRepository repository, INavigationService navigationService)
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
    
    
    private string _nameErrorText = "";

    public string NameErrorText
    {
        get => _nameErrorText;
    
        set
        {
            _nameErrorText = value;
            OnPropertyChanged(nameof(NameErrorText));
        }
    }
    
    public override void Initialize(StatusModel model)
    {
        base.Initialize(model);

        int? id = model?.Id;
        
        Name = "";
        
        if (id == 0) {
            _mode = FormMode.Add;
            
            IsNameValid = false;
            _initialName = "";
        }
        else {
            _initialName = model?.Name!;
            _mode = FormMode.Edit;
            
            Name = model?.Name!;
            
            _isNameValid = true;
        }
    }

    private async void Submit()
    {
        if (await IsNameUnique() == false)
            return;
        
        _model.Name = Name;
        
        if (_mode == FormMode.Add)
            await _repository.AddAsync(_model);
        else
            await _repository.UpdateAsync(_model);
        
        _navigationService.Navigate<AccountingPageViewModel>();
    }

    private bool CanSubmit()
    {
        return IsNameValid;
    }

    private async Task<bool> IsNameUnique()
    {
        if (_mode == FormMode.Edit && _initialName == Name)
            return true;
        
        if (await _repository.GetByNameAsync(Name) != null) {
            NameErrorText = "Name is not unique";
            return false;
        }

        return true;
    }
}