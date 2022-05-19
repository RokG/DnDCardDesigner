using System;
using System.Windows.Input;

namespace CardDesigner.UI.Commands
{
    // make abstract to force overriding
    //https://www.youtube.com/watch?v=bYPU8X8f2xU&t=0s
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}