using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.Services.Repositories.Hardware;
using HardwareAcc.Services.Repositories.Status;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.Forms;

public class HardwareFormPageViewModel : BaseFormViewModel<HardwareModel>
{
    private readonly IHardwareRepository _hardwareRepository;
    private readonly INavigationService _navigationService;
    private readonly IAudienceRepository _audienceRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IUserRepository _userRepository;

    private string _initialInventoryNumber = "";

    public HardwareFormPageViewModel(IHardwareRepository hardwareRepository,
        INavigationService navigationService,
        IAudienceRepository audienceRepository,
        IStatusRepository statusRepository,
        IUserRepository userRepository)
    {
        _hardwareRepository = hardwareRepository;
        _navigationService = navigationService;
        _audienceRepository = audienceRepository;
        _statusRepository = statusRepository;
        _userRepository = userRepository;
        
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
    
    private string _inventoryNumber = "";
    public string InventoryNumber
    {
        get => _inventoryNumber;
    
        set
        {
            _inventoryNumber = value;
            OnPropertyChanged(nameof(InventoryNumber));
        }
    }
    
    private bool _isInventoryNumberValid;
    public bool IsInventoryNumberValid
    {
        get => _isInventoryNumberValid;
    
        set
        {
            _isInventoryNumberValid = value;
            OnPropertyChanged(nameof(IsInventoryNumberValid));
        }
    }
    
    private string _inventoryNumberErrorText = "";
    public string InventoryNumberErrorText
    {
        get => _inventoryNumberErrorText;
    
        set
        {
            _inventoryNumberErrorText = value;
            OnPropertyChanged(nameof(InventoryNumberErrorText));
        }
    }
    
    private string _price = "";
    public string Price
    {
        get => _price;
    
        set
        {
            _price = value;
            OnPropertyChanged(nameof(Price));
        }
    }
    
    private bool _isPriceValid;
    public bool IsPriceValid
    {
        get => _isPriceValid;
    
        set
        {
            _isPriceValid = value;
            OnPropertyChanged(nameof(IsPriceValid));
        }
    }
    
    private ObservableCollection<string> _statusItems = new();
    public ObservableCollection<string> StatusItems
    {
        get => _statusItems;
    
        set
        {
            _statusItems = value;
            OnPropertyChanged(nameof(StatusItems));
        }
    }
    
    private string _statusSelectedItem = "";
    public string StatusSelectedItem
    {
        get => _statusSelectedItem;
    
        set
        {
            _statusSelectedItem = value;
            OnPropertyChanged(nameof(StatusSelectedItem));
        }
    }
    
    private ObservableCollection<string> _audienceItems = new();
    public ObservableCollection<string> AudienceItems
    {
        get => _audienceItems;
    
        set
        {
            _audienceItems = value;
            OnPropertyChanged(nameof(AudienceItems));
        }
    }
    
    private string _audienceSelectedItem = "";
    public string AudienceSelectedItem
    {
        get => _audienceSelectedItem;
    
        set
        {
            _audienceSelectedItem = value;
            OnPropertyChanged(nameof(AudienceSelectedItem));
        }
    }
    
    private ObservableCollection<string> _responsibleUserItems = new();
    public ObservableCollection<string> ResponsibleUserItems
    {
        get => _responsibleUserItems;
    
        set
        {
            _responsibleUserItems = value;
            OnPropertyChanged(nameof(ResponsibleUserItems));
        }
    }
    
    private string _responsibleUserSelectedItem = "";
    public string ResponsibleUserSelectedItem
    {
        get => _responsibleUserSelectedItem;
    
        set
        {
            _responsibleUserSelectedItem = value;
            OnPropertyChanged(nameof(ResponsibleUserSelectedItem));
        }
    }

    #endregion

    public override async void Initialize(HardwareModel model)
    {
        base.Initialize(model);

        int? id = model?.Id;

        Name = "";
        InventoryNumber = "";
        Price = "";
        StatusSelectedItem = "";
        AudienceSelectedItem = "";
        ResponsibleUserSelectedItem = "";

        if (id == 0) 
        {
            _mode = FormMode.Add;

            IsNameValid = false;
            
            IsInventoryNumberValid = false;
            _initialInventoryNumber = "";

            IsPriceValid = true;
        }
        else
        {
            _initialInventoryNumber = model?.InventoryNumber;
         
            _mode = FormMode.Edit;

            Name = model?.Name;
            InventoryNumber = model?.InventoryNumber;
            Price = model?.Price == 0 ? "" : model?.Price.ToString(CultureInfo.InvariantCulture);
            
            IsNameValid = true;
            IsInventoryNumberValid = true;
            IsPriceValid = true;
        }
        
        await ResetStatusItems();
        await ResetAudienceItems();
        await ResetResponsibleUserItems();

        StatusSelectedItem = _model?.StatusName;
        AudienceSelectedItem = _model?.AudienceCode;
        ResponsibleUserSelectedItem = _model?.ResponsibleUserLogin;
    }

    private async Task ResetStatusItems()
    {
        StatusItems.Clear();
        IEnumerable<StatusModel> statusModels = await _statusRepository.GetAllAsync();
        foreach (StatusModel statusModel in statusModels) 
            StatusItems.Add(statusModel.Name);
    }
    
    private async Task ResetAudienceItems()
    {
        AudienceItems.Clear();
        IEnumerable<AudienceModel> audienceModels = await _audienceRepository.GetAllAsync();
        foreach (AudienceModel audienceModel in audienceModels) 
            AudienceItems.Add(audienceModel.Code);
    }
    
    private async Task ResetResponsibleUserItems()
    {
        ResponsibleUserItems.Clear();
        IEnumerable<UserModel> userModels = await _userRepository.GetAllAsync();
        foreach (UserModel userModel in userModels) 
            ResponsibleUserItems.Add(userModel.Login);
    }

    private async void Submit()
    {
        if (await IsInventoryNumberUnique() == false)
            return;

        if (string.IsNullOrEmpty(StatusSelectedItem) == false)
        {
            StatusModel statusModel = await _statusRepository.GetByNameAsync(StatusSelectedItem);
            _model.StatusId = statusModel.Id;
            _model.StatusName = statusModel.Name;
        }
        
        if (string.IsNullOrEmpty(AudienceSelectedItem) == false)
        {
            AudienceModel audienceModel = await _audienceRepository.GetByCodeAsync(AudienceSelectedItem);
            _model.AudienceId = audienceModel.Id;
            _model.AudienceCode = audienceModel.Code;
        }
        
        if (string.IsNullOrEmpty(ResponsibleUserSelectedItem) == false)
        {
            UserModel userModel = await _userRepository.GetByLoginAsync(ResponsibleUserSelectedItem);
            _model.ResponsibleUserId = userModel.Id;
            _model.ResponsibleUserLogin = userModel.Login;
        }
        
        _model.Name = Name;
        _model.InventoryNumber = InventoryNumber;
        
        if (string.IsNullOrEmpty(Price) == false)
            _model.Price = Convert.ToDouble(Price);
        
        if (_mode == FormMode.Add)
            await _hardwareRepository.AddAsync(_model);
        else
            await _hardwareRepository.UpdateAsync(_model);

        _navigationService.Navigate<AccountingPageViewModel>();
    }

    private bool CanSubmit() => 
        IsNameValid && IsInventoryNumberValid && IsPriceValid;


    private async Task<bool> IsInventoryNumberUnique()
    {
        if (_mode == FormMode.Edit && _initialInventoryNumber == InventoryNumber)
            return true;
        
        if (await _hardwareRepository.GetByInventoryNumberAsync(InventoryNumber) != null) 
        {
            InventoryNumberErrorText = "Inventory Number is not unique";
            return false;
        }

        return true;
    }
}