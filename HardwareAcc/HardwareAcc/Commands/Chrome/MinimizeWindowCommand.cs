using System.Windows;

namespace HardwareAcc.Commands.Chrome;

public class MinimizeWindowCommand : BaseCommand
{
    private readonly Window _window;

    public MinimizeWindowCommand(Window window)
    {
        _window = window;
    }

    public override void Execute(object parameter)
    {
        _window.WindowState = WindowState.Minimized;
    }
}