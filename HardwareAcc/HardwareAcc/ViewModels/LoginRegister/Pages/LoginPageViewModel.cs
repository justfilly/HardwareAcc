using System.Diagnostics;

namespace HardwareAcc.ViewModels.LoginRegister.Pages;

public class LoginPageViewModel : BaseViewModel
{
    private string _username = "";
    public string Username
    {
        get => _username;
    
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
            Debug.WriteLine("xxxxxxxxxxxxxxxxx");
            Debug.WriteLine("xxxxxxxxxxxxxxxxx");
            Debug.WriteLine(_username);
            Debug.WriteLine("xxxxxxxxxxxxxxxxx");
            Debug.WriteLine("xxxxxxxxxxxxxxxxx");
        }
    }
}