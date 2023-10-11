using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    internal class AddHolidayStateCommand : CommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        public AddHolidayStateCommand(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }

        public override void Execute(object parameter)
        {
            _jewishHolidayViewModel.AddingHolidayVisibility = "Visible";
        }
    }
}
