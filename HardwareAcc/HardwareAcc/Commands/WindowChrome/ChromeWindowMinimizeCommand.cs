using System.Windows;

namespace HardwareAcc.Commands.WindowChrome;

public class ChromeWindowMinimizeCommand : BaseCommand
{
    private WindowState _windowState;

    public ChromeWindowMinimizeCommand(WindowState windowState)
    {
        _windowState = windowState;
    }

    public override void Execute(object? parameter) => 
        _windowState = WindowState.Minimized;
}