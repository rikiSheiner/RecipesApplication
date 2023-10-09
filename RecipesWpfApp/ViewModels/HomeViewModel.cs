using RecipesWpfApp.Commands;
using RecipesWpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipesWpfApp.ViewModels
{
    internal class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateSearchRecipeCommand { get; set; }
        public ICommand NavigateRecipesBookCommand { get; set; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateSearchRecipeCommand = new NavigateSearchRecipeCommand(navigationStore);
            NavigateRecipesBookCommand = new NavigateRecipesBookCommand(navigationStore);
        }

    }
}
