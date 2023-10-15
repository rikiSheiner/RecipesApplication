using RecipesWpfApp.Commands;
using RecipesWpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Services;

namespace RecipesWpfApp.ViewModels
{
    internal class HomeViewModel : ViewModelBase
    {
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ICommand NavigateSearchRecipesCommand { get; set; }
        public ICommand NavigateRecipesBookCommand { get; set; }

        public HomeViewModel(NavigationBarViewModel navigationBarViewModel, //NavigationStore navigationStore,
            NavigationService<SearchRecipeViewModel> searchRecipeNavigationService,
            NavigationService<RecipesBookViewModel> recipesBookNavigationService)
        { 
            NavigationBarViewModel = navigationBarViewModel;
            NavigateSearchRecipesCommand = new NavigateCommand<SearchRecipeViewModel>(searchRecipeNavigationService);
            NavigateRecipesBookCommand = new NavigateCommand<RecipesBookViewModel>(recipesBookNavigationService);
        }

    }
}
