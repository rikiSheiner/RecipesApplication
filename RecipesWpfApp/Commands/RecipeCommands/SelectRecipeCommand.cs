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
    internal class SelectRecipeCommand : CommandBase
    {
        private SearchRecipeViewModel _viewModel;
        private readonly ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> _navigationService;


        public SelectRecipeCommand(SearchRecipeViewModel searchRecipeViewModel,
            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService)
        {
            _viewModel = searchRecipeViewModel;
            _navigationService = navigationService;
        }



        public override void Execute(object parameter)
        {
            RecipeDetails recipeDetails = _viewModel.SelectedRecipeDetails;
            _navigationService.Navigate(recipeDetails);
        }
    }
}
