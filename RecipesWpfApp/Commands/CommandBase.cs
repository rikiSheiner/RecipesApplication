using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipesWpfApp.Commands
{
    /// <summary>
    /// מחלקה השמשמשת לייצוג של פקודה כללית סינכרונית
    /// </summary>
    public abstract class CommandBase : ICommand
    {

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        // הפעולה שמתבצעת בעת הרצת הפקודה 
        public abstract void Execute(object parameter);

        // ברגע שמתרחש האירוע המתאים מריצים את הפקודה 
        protected virtual void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
