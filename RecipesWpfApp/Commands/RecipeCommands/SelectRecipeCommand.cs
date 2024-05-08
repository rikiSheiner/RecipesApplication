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
    /// מחלקה המשמשת לייצוג פקודה של בחירת מתכון לא שמור
    /// זה יגרום לניווט לעמוד המתכון המתאים
    /// </summary>
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


        // הפעולה המתבצעת בעת הרצת פקודת בחירת מתכון מרשימת המתכונים הלא שמורים
        // יתבצע ניווט לעמוד מתכון יחיד עם פרטי מתכון זה
        public override void Execute(object parameter)
        {
            RecipeDetails recipeDetails = _viewModel.SelectedRecipeDetails;
            _navigationService.Navigate(recipeDetails);
        }
    }
}
