using HardwareAcc.Commands;
using HardwareAcc.MVVM.ViewModels.LoginRegister;
using HardwareAcc.Services.DBConnection;
using HardwareAcc.Services.Navigation;

namespace HardwareAcc.MVVM.ViewModels.DBConnectionError;

public class DBConnectionErrorPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IDBConnectionService _dbConnectionService;

    public DBConnectionErrorPageViewModel(INavigationService navigationService, IDBConnectionService dbConnectionService)
    {
        _navigationService = navigationService;
        _dbConnectionService = dbConnectionService;

        RetryCommand = new RelayCommand(Retry, () => true);
    }

    public RelayCommand RetryCommand { get; }

    private void Retry()
    {
        if (_dbConnectionService.CheckForConnection() == false)
            return;
        
        _navigationService.Navigate<LoginPageViewModel>();
    }
}