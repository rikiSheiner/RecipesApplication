using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesWpfApp.Services;

namespace RecipesWpfApp.Commands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של ניווט בין העמודים
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    internal class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        // השירות המאפשר מעבר בין העמודים
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        // ביצוע פעולת הניווט בעת הצורך
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
