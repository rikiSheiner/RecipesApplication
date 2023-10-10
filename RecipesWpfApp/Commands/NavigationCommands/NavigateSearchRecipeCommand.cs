using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NavigationCommands
{
    internal class NavigateSearchRecipeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NavigateSearchRecipeCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new SearchRecipeViewModel(_navigationStore);
        }
    }
}
