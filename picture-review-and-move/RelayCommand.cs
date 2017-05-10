using System;
using System.Windows.Input;

namespace picture_review_and_move
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;

        private readonly Func<bool> _canExecute;

        public RelayCommand(Action action) : this(action, () => true) { }

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute();

        public void Execute(object parameter) => _action();

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var canExecuteChanged = CanExecuteChanged;
            canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}