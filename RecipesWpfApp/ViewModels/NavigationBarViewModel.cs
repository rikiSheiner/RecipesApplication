using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Services;

namespace RecipesWpfApp.ViewModels
{
    internal class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSearchRecipesCommand { get; }
        public ICommand NavigateRecipesBookCommand { get; }

        public NavigationBarViewModel(NavigationService<HomeViewModel> homeNavigationService,
            NavigationService<SearchRecipeViewModel> searchRecipesNavigationService,
            NavigationService<RecipesBookViewModel> recipesBookNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            NavigateSearchRecipesCommand = new NavigateCommand<SearchRecipeViewModel>(searchRecipesNavigationService);
            NavigateRecipesBookCommand = new NavigateCommand<RecipesBookViewModel>(recipesBookNavigationService);
        }

    }
}
