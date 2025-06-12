using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VectorGraphicsViewer.Presentation.Helpers
{
    /// <summary>
    /// ICommand implementation used for asynchronous operations.
    /// Used in ViewModels within the MVVM pattern to define commands.
    /// </summary>
    public class RelayCommand : ICommand
    {
        // Business logic to be executed asynchronously (e.g., loading a file)
        private readonly Func<Task> _execute;

        // Condition that determines whether the command can execute
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// RelayCommand constructor.
        /// </summary>
        /// <param name="execute">Method to be executed asynchronously</param>
        /// <param name="canExecute">Optional condition to determine if the command can run</param>
        public RelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (() => true); // if null, always executable
        }

        /// <summary>
        /// Determines whether the command can execute (e.g., button enabled/disabled).
        /// </summary>
        public bool CanExecute(object parameter) => _canExecute();

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        public async void Execute(object parameter) => await _execute();

        /// <summary>
        /// Notifies the UI to re-check the command's executability when it changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
