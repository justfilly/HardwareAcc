using System.Windows;

namespace HardwareAcc.Commands.Chrome;

public class CloseWindowCommand : BaseCommand
{
    private readonly Window _window;

    public CloseWindowCommand(Window window)
    {
        _window = window;
    }

    public override void Execute(object parameter) => 
        _window.Close();
}