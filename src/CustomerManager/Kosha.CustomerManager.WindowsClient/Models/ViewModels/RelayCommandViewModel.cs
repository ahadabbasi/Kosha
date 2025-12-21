using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosha.CustomerManager.WindowsClient.Models.ViewModels;

public class RelayCommandViewModel<T>(
    Action<T> execute, 
    Predicate<T>? canExecute = null
) : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;

        remove => CommandManager.RequerySuggested -= value;
    }

    public virtual bool CanExecute(object? parameter = null)
        => canExecute == null || (parameter is not null && parameter is T entry && canExecute(entry));
    

    public virtual void Execute(object? parameter = null)
    {
        if(parameter is not null && parameter is T entry)
            execute(entry);
    }
}

public class RelayCommandViewModel(
    Action<ValueTask> execute,
    Predicate<ValueTask>? canExecute = null
) : RelayCommandViewModel<ValueTask>(execute, canExecute)
{
    public override bool CanExecute(object? parameter = null)
    {
        return base.CanExecute(ValueTask.CompletedTask);
    }

    public override void Execute(object? parameter = null)
    {
        base.Execute(ValueTask.CompletedTask);
    }
}