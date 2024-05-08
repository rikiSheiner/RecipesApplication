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
    /// <summary>
    /// מחלקה המשמשת  לייצוג פקודה המתבצעת בעת בחירת 
    /// מתכון מרשימת המתכונים השמורים
    /// </summary>
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


        // פעולה המתבצעת בעת הרצת הפקודה של בחירת מתכון שמור
        // יתבצע ניווט לעמוד המתכון המתאים
        public override void Execute(object parameter)
        {
            RecipeDetails recipeDetails = _viewModel.SelectedRecipe;
            _navigationService.Navigate(recipeDetails);
        }
    }
}
