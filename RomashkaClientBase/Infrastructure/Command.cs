using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RomashkaClientBase
{
    public class Command : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add     => CommandManager.RequerySuggested += value; 
            remove  => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
            => (canExecute == null) || canExecute(parameter);
        
        public void Execute(object parameter)
            => execute?.Invoke(parameter);

        public Command(Action<object> execute, Func<object,bool> canExecute)
        {
            this.execute    = execute;
            this.canExecute = canExecute;
        }

        public Command(Action<object> execute)
            : this(execute, null)
        { }
    }
}
