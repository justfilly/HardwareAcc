using System.Windows;
using HardwareAcc.Commands.Chrome;

namespace HardwareAcc.MVVM.ViewModels;

public abstract class ChromeWindowViewModel : BaseViewModel
{
    protected ChromeWindowViewModel(Window window)
    {
        MinimizeWindowCommand = new MinimizeWindowCommand(window);
        MaximizeWindowCommand = new MaximizeWindowCommand(window);
        CloseWindowCommand = new CloseWindowCommand(window);
    }

    public MinimizeWindowCommand MinimizeWindowCommand { get; }
    public MaximizeWindowCommand MaximizeWindowCommand { get; }
    public CloseWindowCommand CloseWindowCommand { get; }
}