using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Models;

namespace RecipesWpfApp.ViewModels
{
    internal class JewishHolidayViewModel: ViewModelBase
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
        public ICommand AddJewishHolidayCommand;
        public ICommand RemoveJewishHolidayCommand;

        JewishHolidayViewModel(ICommand addJewishHolidayCommand, ICommand removeJewishHolidayCommand)
        {
            AddJewishHolidayCommand = addJewishHolidayCommand;
            RemoveJewishHolidayCommand = removeJewishHolidayCommand;
        }
    }
}
