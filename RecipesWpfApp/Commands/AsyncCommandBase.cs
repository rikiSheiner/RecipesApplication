using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands
{
    /// <summary>
    /// מחלקה מופשטת המשמשת לייצוג מבנה כללי של מחלקת פקודה 
    /// הרצה בצורה אסינכרונית
    /// </summary>
    public abstract class AsyncCommandBase : CommandBase
    {
        // האם הפקודה רצה כעת
        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                OnCanExecutedChanged();
            }
        }

        // האם הפקודה יכולה לרוץ
        public override bool CanExecute(object parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        // הרצה של הפקודה
        public override async void Execute(object parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception) { }
            finally
            {
                IsExecuting = false;
            }
        }

        // הפעולה שמתבצעת בעת הרצת הפקודה האסינכרונית
        public abstract Task ExecuteAsync(object parameter);
    }
}
