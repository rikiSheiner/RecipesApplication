using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands
{
    internal class NavigateRecipesBookCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NavigateRecipesBookCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new RecipesBookViewModel();
        }
    }

}
