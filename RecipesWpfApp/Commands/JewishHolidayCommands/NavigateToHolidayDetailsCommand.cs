using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    internal class NavigateToHolidayDetailsCommand : CommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        private NavigationStore _navigationStore;
        public NavigateToHolidayDetailsCommand(JewishHolidayViewModel jewishHolidayViewModel,
            NavigationStore navigationStore)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new JewishHolidayDetailsViewModel(_jewishHolidayViewModel, _navigationStore);
        }
    }
}
