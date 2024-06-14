using System.Windows;

namespace HardwareAcc.Commands.WindowChrome;

public class ChromeWindowCloseCommand : BaseCommand
{
    private readonly Window _window;

    public ChromeWindowCloseCommand(Window window)
    {
        _window = window;
    }

    public override void Execute(object? parameter) => 
        _window.Close();
}