using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DayNightPapers.Helpers
{
    /// <summary>
    /// A simple wrapper class for turning events into ICommand
    /// CanEexecute will always return true, it's assumes that the 
    /// EventHandler checks if it should run itself.
    /// </summary>
    class ClickEventWrapper : ICommand
    {
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// The event which is raised when .Execute is runs
        /// </summary>
        public event EventHandler Clicked;

        public ClickEventWrapper(EventHandler click)
        {
            Clicked = click;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}
