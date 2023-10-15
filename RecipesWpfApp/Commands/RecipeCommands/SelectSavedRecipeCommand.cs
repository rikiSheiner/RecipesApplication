using RecipesWpfApp.Models;
using RecipesWpfApp.Services;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.RecipeCommands
{
    internal class SelectSavedRecipeCommand : CommandBase
    {
        private RecipesBookViewModel _viewModel;
        private readonly ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> _navigationService;


        public SelectSavedRecipeCommand(RecipesBookViewModel recipesBookViewModel, 
            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService) 
        {
            _viewModel = recipesBookViewModel;
            _navigationService = navigationService;
        }



        public override void Execute(object parameter)
        {
            RecipeDetails recipeDetails = _viewModel.SelectedRecipe;
            _navigationService.Navigate(recipeDetails);
        }
    }
}
