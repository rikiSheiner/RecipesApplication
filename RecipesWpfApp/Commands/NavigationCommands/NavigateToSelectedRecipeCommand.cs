using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using RecipesWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NavigationCommands
{
    internal class NavigateToSelectedRecipeCommand : CommandBase
    {
        private NavigationStore _navigationStore; 
        private RecipeDetails _recipeDetails;
        private SearchRecipeViewModel _searchRecipeViewModel = null;
        private RecipesBookViewModel _recipesBookViewModel = null;

        public NavigateToSelectedRecipeCommand(NavigationStore navigationStore, SearchRecipeViewModel searchRecipeViewModel)
        {
            _navigationStore = navigationStore;
            _searchRecipeViewModel = searchRecipeViewModel;
        }

        public NavigateToSelectedRecipeCommand(NavigationStore navigationStore, RecipesBookViewModel recipesBookViewModel)
        {
            _navigationStore = navigationStore;
            _recipesBookViewModel = recipesBookViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_searchRecipeViewModel != null)
                _recipeDetails = _searchRecipeViewModel.SelectedRecipeDetails;
            else
                _recipeDetails = _recipesBookViewModel.SelectedRecipe;

            bool saved;

            if (bool.TryParse((string)parameter, out saved))
                _navigationStore.CurrentViewModel = new SingleRecipeViewModel(_recipeDetails, saved);
            else
                _navigationStore.CurrentViewModel = new SingleRecipeViewModel(_recipeDetails, false);
        }

    }
}
