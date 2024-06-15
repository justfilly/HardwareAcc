using System.Windows;
using HardwareAcc.Commands.Chrome;

namespace HardwareAcc.ViewModels;

public class ChromeWindowViewModel : BaseViewModel
{
    public ChromeWindowViewModel(Window window)
    {
        MinimizeWindowCommand = new MinimizeWindowCommand(window);
        MaximizeWindowCommand = new MaximizeWindowCommand(window);
        CloseWindowCommand = new CloseWindowCommand(window);
    }

    public MinimizeWindowCommand MinimizeWindowCommand { get; }
    public MaximizeWindowCommand MaximizeWindowCommand { get; }
    public CloseWindowCommand CloseWindowCommand { get; }
}