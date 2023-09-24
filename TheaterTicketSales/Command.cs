using System;
using System.Windows.Input;

namespace TheaterTicketSales
{
    internal class Command : ICommand
    {
        private Action<object> _action;

        public Command(Action<object> action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action(parameter);
        }
    }
}