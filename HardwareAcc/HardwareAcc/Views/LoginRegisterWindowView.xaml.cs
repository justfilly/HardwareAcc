using HardwareAcc.ViewModels;

namespace HardwareAcc.Views;

public partial class LoginRegisterWindowView
{
    public LoginRegisterWindowView(LoginRegisterWindowViewModel loginRegisterWindowViewModel)
    {
        InitializeComponent();
        DataContext = loginRegisterWindowViewModel;
    }
}