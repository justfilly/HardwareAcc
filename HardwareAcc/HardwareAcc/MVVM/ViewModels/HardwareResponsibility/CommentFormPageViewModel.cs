using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms.Base;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.Hardware;
using HardwareAcc.Services.Repositories.HardwareResponsibilityHistory;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.HardwareResponsibility;

public class CommentFormPageViewModel : BaseFormViewModel<HardwareResponsibilityHistoryModel>
{
    private readonly INavigationService _navigationService;
    private readonly IHardwareRepository _hardwareRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHardwareResponsibilityHistoryRepository _hardwareResponsibilityHistoryRepository;

    public CommentFormPageViewModel(INavigationService navigationService,
        IHardwareResponsibilityHistoryRepository hardwareResponsibilityHistoryRepository, 
        IUserRepository userRepository, 
        IHardwareRepository hardwareRepository)
    {
        _navigationService = navigationService;
        _hardwareResponsibilityHistoryRepository = hardwareResponsibilityHistoryRepository;
        _userRepository = userRepository;
        _hardwareRepository = hardwareRepository;

        HardwareResponsibilityNavigateCommand =
            new NavigateCommand<HardwareResponsibilityPageViewModel>(_navigationService);

        SubmitCommand = new RelayCommand(Submit);
    }

    public NavigateCommand<HardwareResponsibilityPageViewModel> HardwareResponsibilityNavigateCommand { get; }

    public RelayCommand SubmitCommand { get; }

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

    private string _commentText = "";

    public string CommentText
    {
        get => _commentText;
    
        set
        {
            _commentText = value;
            OnPropertyChanged(nameof(CommentText));
        }
    }

    public override async void Initialize(HardwareResponsibilityHistoryModel model)
    {
        base.Initialize(model);

        HardwareModel hardwareModel = await _hardwareRepository.GetByIdAsync(model.HardwareId);
        HardwareName = hardwareModel.Name;
        
        UserModel user = await _userRepository.GetByIdAsync(model.ResponsibleUserId);
        HardwareResponsibleUser = user.Login;

        CommentText = _model.Comment;
    }

    private async void Submit()
    {
        _model.Comment = CommentText;
        await _hardwareResponsibilityHistoryRepository.UpdateAsync(_model);
        _navigationService.Navigate<HardwareResponsibilityPageViewModel>();
    }
}