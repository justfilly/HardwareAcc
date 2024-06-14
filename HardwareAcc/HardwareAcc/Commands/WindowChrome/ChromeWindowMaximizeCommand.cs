using System.Windows;

namespace HardwareAcc.Commands.WindowChrome;

public class ChromeWindowMaximizeCommand : BaseCommand
{
    private WindowState _windowState;

    public ChromeWindowMaximizeCommand(WindowState windowState)
    {
        _windowState = windowState;
    }

    public override void Execute(object? parameter)
    {
        if (_windowState == WindowState.Maximized)
            _windowState = WindowState.Normal;
        else
            _windowState = WindowState.Maximized;
    }
}