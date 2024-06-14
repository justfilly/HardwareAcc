using System.Windows;
using System.Windows.Input;
using HardwareAcc.Commands.WindowChrome;

namespace HardwareAcc.ViewModels;

public class ChromeWindowViewModel : BaseViewModel
{
    public ChromeWindowViewModel(Window window)
    {
        WindowCloseCommand = new ChromeWindowCloseCommand(window);
        WindowMaximizeCommand = new ChromeWindowMaximizeCommand(window.WindowState);
        WindowMinimizeCommand = new ChromeWindowMinimizeCommand(window.WindowState);
    }

    public ICommand WindowCloseCommand { get; }
    public ICommand WindowMaximizeCommand { get; }
    public ICommand WindowMinimizeCommand { get; }
}