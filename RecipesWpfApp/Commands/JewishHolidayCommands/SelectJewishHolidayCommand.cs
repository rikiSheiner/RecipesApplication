using RecipesWpfApp.Models;
using RecipesWpfApp.Services;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של בחירת מועד שבו הוכן המתכון 
    /// </summary>
    internal class SelectJewishHolidayCommand : CommandBase
    {
        private JewishHolidayViewModel _viewModel;
        private readonly ParameterNavigationService<JewishHoliday, JewishHolidayDetailsViewModel> _navigationService;


        public SelectJewishHolidayCommand(JewishHolidayViewModel jewishHolidayViewModel,
            ParameterNavigationService<JewishHoliday, JewishHolidayDetailsViewModel> navigationService)
        {
            _viewModel = jewishHolidayViewModel;
            _navigationService = navigationService;
        }


        public override void Execute(object parameter)
        {
            JewishHoliday holiday = _viewModel.SelectedJewishHoliday;
            _navigationService.Navigate(holiday);
        }
    }
}
