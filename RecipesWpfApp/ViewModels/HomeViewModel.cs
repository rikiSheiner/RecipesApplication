using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.NavigationCommands;
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
        private readonly NavigationStore _navigationStore;
        public ICommand NavigateSearchRecipeCommand { get; set; }
        public ICommand NavigateRecipesBookCommand { get; set; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            NavigateSearchRecipeCommand = new NavigateSearchRecipeCommand(_navigationStore);
            NavigateRecipesBookCommand = new NavigateRecipesBookCommand(_navigationStore);
        }

    }
}
