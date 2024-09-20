using System;
using System.Windows.Input;

namespace HardwareAcc.Commands;

public class RelayCommandWithParameter : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommandWithParameter(Action<object> execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object? parameter)
    {
        _execute(parameter!);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}