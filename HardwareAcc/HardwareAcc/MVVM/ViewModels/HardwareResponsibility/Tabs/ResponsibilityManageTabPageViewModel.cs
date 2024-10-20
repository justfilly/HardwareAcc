using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.Services.Repositories.Hardware;
using HardwareAcc.Services.Repositories.HardwareResponsibilityHistory;
using HardwareAcc.Services.Repositories.Status;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.HardwareResponsibility.Tabs;

public class ResponsibilityManageTabPageViewModel : BaseFormViewModel<HardwareModel>
{
    private readonly INavigationService _navigationService;
    private readonly IStatusRepository _statusRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAudienceRepository _audienceRepository;
    private readonly IHardwareRepository _hardwareRepository;
    private readonly IHardwareResponsibilityHistoryRepository _hardwareResponsibilityHistoryRepository;
    
    public ResponsibilityManageTabPageViewModel(INavigationService navigationService,
        IStatusRepository statusRepository,
        IUserRepository userRepository,
        IAudienceRepository audienceRepository, 
        IHardwareResponsibilityHistoryRepository hardwareResponsibilityHistoryRepository, 
        IHardwareRepository hardwareRepository)
    {
        _navigationService = navigationService;
        _statusRepository = statusRepository;
        _userRepository = userRepository;
        _audienceRepository = audienceRepository;
        _hardwareResponsibilityHistoryRepository = hardwareResponsibilityHistoryRepository;
        _hardwareRepository = hardwareRepository;

        NavigateToCommentFormCommand = new RelayCommand(EditComment);
        TransferResponsibilityCommand = new RelayCommand(TransferResponsibility);
    }

    public RelayCommand NavigateToCommentFormCommand { get; }
    public RelayCommand TransferResponsibilityCommand { get; }

    #region Props

    private string _hardwareName = "";

    public string HardwareName
    {
        get => _hardwareName;
    
        set
        {
            _hardwareName = value;
            OnPropertyChanged(nameof(HardwareName));
        }
    }

    private string _hardwareInventoryNumber = "";

    public string HardwareInventoryNumber
    {
        get => _hardwareInventoryNumber;
    
        set
        {
            _hardwareInventoryNumber = value;
            OnPropertyChanged(nameof(HardwareInventoryNumber));
        }
    }

    private string _hardwarePrice = "";

    public string HardwarePrice
    {
        get => _hardwarePrice;
    
        set
        {
            _hardwarePrice = value;
            OnPropertyChanged(nameof(HardwarePrice));
        }
    }

    private string _hardwareStatus = "";

    public string HardwareStatus
    {
        get => _hardwareStatus;
    
        set
        {
            _hardwareStatus = value;
            OnPropertyChanged(nameof(HardwareStatus));
        }
    }

    private string _hardwareResponsibleUser = "";

    public string HardwareResponsibleUser
    {
        get => _hardwareResponsibleUser;
    
        set
        {
            _hardwareResponsibleUser = value;
            OnPropertyChanged(nameof(HardwareResponsibleUser));
        }
    }

    private string _hardwareAudience = "";

    public string HardwareAudience
    {
        get => _hardwareAudience;
    
        set
        {
            _hardwareAudience = value;
            OnPropertyChanged(nameof(HardwareAudience));
        }
    }

    private ObservableCollection<string> _userCommentItems = new();

    public ObservableCollection<string> UserCommentItems
    {
        get => _userCommentItems;
    
        set
        {
            _userCommentItems = value;
            OnPropertyChanged(nameof(UserCommentItems));
        }
    }

    private string _userCommentSelectedItem = "";

    public string UserCommentSelectedItem
    {
        get => _userCommentSelectedItem;
    
        set
        {
            _userCommentSelectedItem = value;
            OnPropertyChanged(nameof(UserCommentSelectedItem));
        }
    }

    private string _userCommentErrorText = "";

    public string UserCommentErrorText
    {
        get => _userCommentErrorText;
    
        set
        {
            _userCommentErrorText = value;
            OnPropertyChanged(nameof(UserCommentErrorText));
        }
    }

    private ObservableCollection<string> _userResponsibilityItems = new();

    public ObservableCollection<string> UserResponsibilityItems
    {
        get => _userResponsibilityItems;
    
        set
        {
            _userResponsibilityItems = value;
            OnPropertyChanged(nameof(UserResponsibilityItems));
        }
    }

    private string _userResponsibilitySelectedItem = "";

    public string UserResponsibilitySelectedItem
    {
        get => _userResponsibilitySelectedItem;
    
        set
        {
            _userResponsibilitySelectedItem = value;
            OnPropertyChanged(nameof(UserResponsibilitySelectedItem));
        }
    }

    private string _userResponsibilityErrorText = "";

    public string UserResponsibilityErrorText
    {
        get => _userResponsibilityErrorText;
    
        set
        {
            _userResponsibilityErrorText = value;
            OnPropertyChanged(nameof(UserResponsibilityErrorText));
        }
    }

    #endregion

    public override async void Initialize(HardwareModel model)
    {
        base.Initialize(model);
        
        HardwareName = model.Name;
        HardwareInventoryNumber = model.InventoryNumber;
        HardwarePrice = model.Price.ToString();
        
        HardwareStatus =  model.StatusName;
        HardwareResponsibleUser = model.ResponsibleUserLogin;

        HardwareAudience = model.AudienceCode;
        
        await ResetUserCommentItems();
        await ResetUserResponsibilityItems();
    }

    private async Task ResetUserCommentItems()
    {
        UserCommentItems.Clear();
    
        IEnumerable<HardwareResponsibilityHistoryModel> model = 
            await _hardwareResponsibilityHistoryRepository.GetAllByHardwareIdAsync(_model.Id);
    
        var sortedModel = model.OrderByDescending(m => m.ResponsibilityStartDate);

        foreach (HardwareResponsibilityHistoryModel historyModel in sortedModel)
        {
            UserModel userModel = await _userRepository.GetByIdAsync(historyModel.ResponsibleUserId);

            string endDateText = historyModel.ResponsibilityEndDate == DateTime.MinValue 
                ? "Now" 
                : historyModel.ResponsibilityEndDate.ToString("dd/MM/yyyy HH:mm:ss");

            string itemText = $"{userModel.Login}, {historyModel.ResponsibilityStartDate:dd/MM/yyyy HH:mm:ss} - {endDateText}";
            UserCommentItems.Add(itemText);
        }
    }

    private async Task ResetUserResponsibilityItems()
    {
        UserResponsibilityItems.Clear();
        IEnumerable<UserModel> userModels = await _userRepository.GetAllAsync();
        foreach (UserModel userModel in userModels) 
            UserResponsibilityItems.Add(userModel.Login);
    }

    private async void EditComment()
    {
        string selectedItem = UserCommentSelectedItem;

        if (!string.IsNullOrEmpty(selectedItem) && selectedItem.Contains(","))
        {
            string userLogin = selectedItem.Substring(0, selectedItem.IndexOf(",")).Trim();
            
            string[] parts = selectedItem.Split(new[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
        
            if (parts.Length > 1)
            {
                string startDateString = parts[1].Trim();
                DateTime startDate = DateTime.ParseExact(startDateString, "dd/MM/yyyy HH:mm:ss", null);
            
                UserModel userModel = await _userRepository.GetByLoginAsync(userLogin);
                int userId = userModel.Id;

                HardwareResponsibilityHistoryModel model =
                    await _hardwareResponsibilityHistoryRepository.GetByUserIdAndStartDateAsync(userId, startDate);

                _navigationService.NavigateToForm<CommentFormPageViewModel, HardwareResponsibilityHistoryModel>(model);
            }
        }
    }

    private async void TransferResponsibility()
    {
        HardwareResponsibilityHistoryModel previousHistoryModel = await _hardwareResponsibilityHistoryRepository.GetWithLatestStartDateByHardwareIdAsync(_model.Id);

        if (previousHistoryModel != null)
        {
            previousHistoryModel.ResponsibilityEndDate = DateTime.Now;
            await _hardwareResponsibilityHistoryRepository.UpdateAsync(previousHistoryModel);
        }
        
        UserModel newResponsibleUser = await _userRepository.GetByLoginAsync(UserResponsibilitySelectedItem);
        HardwareResponsibilityHistoryModel newHistoryModel = new()
        {
            HardwareId = _model.Id,
            ResponsibleUserId = newResponsibleUser.Id,
            ResponsibilityStartDate = DateTime.Now,
        };

        await _hardwareResponsibilityHistoryRepository.AddAsync(newHistoryModel);

        _model.ResponsibleUserId = newResponsibleUser.Id;
        await _hardwareRepository.UpdateAsync(_model);
        HardwareResponsibleUser = newResponsibleUser.Login;
        
        await ResetUserCommentItems();
    }
}