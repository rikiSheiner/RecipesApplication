using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.RecipesListCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של חיפוש מתכון
    /// מתוך רשימת המתכונים השמורים 
    /// </summary>
    internal class SearchSavedRecipesCommand : CommandBase
    {
        // מופע של האוביקט המשמש לייצוג הלוגיקה של המתכונים השמורים במערכת
        private RecipesBookViewModel recipesBookViewModel;

        public SearchSavedRecipesCommand(RecipesBookViewModel recipesBookViewModel)
        {
            this.recipesBookViewModel = recipesBookViewModel;
        }

        // הפעולה המתבצעת בעת הרצת הפקודה של חיפוש מתכון מהמתכונים השמורים
        public override void Execute(object parameter)
        {
            // מסננים את המתכונים המוצגים על פי שאילתת החיפוש
            string searchValue = recipesBookViewModel.Query;
            recipesBookViewModel.Recipes = recipesBookViewModel.AllRecipes.
                FindAll(recipe => recipe.Name.Contains(searchValue) || recipe.Description.Contains(searchValue)
                || recipe.Ingredients.Exists(ingredient => ingredient.Name.Contains(searchValue)));

        }
    }
}
