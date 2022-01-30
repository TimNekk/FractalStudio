using System;
using System.Windows.Input;

namespace FractalStudio.Core
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;
        
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter) => _canExecute is null || _canExecute(parameter);

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

    }
}