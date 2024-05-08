using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג מצב של הוספת מועד למתכון
    /// האם אנו בתהליך הוספה או לא
    /// </summary>
    internal class AddHolidayStateCommand : CommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        public AddHolidayStateCommand(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }

        // כשמתבצע תהליך הוספה משתנה המצב בהתאם
        public override void Execute(object parameter)
        {
            _jewishHolidayViewModel.IsInAddingHoliday = true;
        }
    }
}
