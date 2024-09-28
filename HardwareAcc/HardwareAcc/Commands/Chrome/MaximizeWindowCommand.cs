using System.Windows;

namespace HardwareAcc.Commands.Chrome;

public class MaximizeWindowCommand : BaseCommand
{
    private readonly Window _window;

    public MaximizeWindowCommand(Window window)
    {
        _window = window;
    }

    public override void Execute(object parameter)
    {
        if (_window.WindowState == WindowState.Maximized)
            _window.WindowState = WindowState.Normal;
        else
            _window.WindowState = WindowState.Maximized;
    }
}