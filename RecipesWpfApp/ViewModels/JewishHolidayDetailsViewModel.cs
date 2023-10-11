using RecipesWpfApp.Models;
using RecipesWpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.ViewModels
{
    internal class JewishHolidayDetailsViewModel : ViewModelBase
    {
        private JewishHoliday _jewishHoliday;
        public JewishHoliday JewishHoliday
        {
            get { return _jewishHoliday; }
            set
            {
                if (_jewishHoliday != value)
                {
                    _jewishHoliday = value;
                    OnPropertyChanged(nameof(JewishHoliday));
                }
            }
        }

        private NavigationStore _navigationStore;
        private JewishHolidayViewModel _jewishHolidayViewModel;

        public JewishHolidayDetailsViewModel(JewishHolidayViewModel jewishHolidayViewModel, NavigationStore navigationStore)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
            _navigationStore = navigationStore;

            JewishHoliday = _jewishHolidayViewModel.SelectedJewishHoliday;
        }


    }
}
