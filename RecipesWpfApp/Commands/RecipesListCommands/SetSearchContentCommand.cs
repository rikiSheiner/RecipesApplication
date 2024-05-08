using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.RecipesListCommands
{
    /// <summary>
    /// מחלקה המשמשת לאיפוס תוכן שאילתת החיפוש של מתכונים
    /// זו מחלקה המשמשת לשני עמודים 
    /// גם לעמוד של מתכונים שמורים וגם לעמוד של מתכונים לא שמורים
    /// </summary>
    internal class SetSearchContentCommand : CommandBase
    {
        private RecipesBookViewModel _recipesBookViewModel;
        private SearchRecipeViewModel _searchRecipeViewModel;

        public SetSearchContentCommand(RecipesBookViewModel recipesBookViewModel)
        {
            _recipesBookViewModel = recipesBookViewModel;
        }

        public SetSearchContentCommand(SearchRecipeViewModel searchRecipeViewModel)
        {
            _searchRecipeViewModel = searchRecipeViewModel;
        }

        public override void Execute(object parameter)
        {
            if(_recipesBookViewModel != null)
                _recipesBookViewModel.Query = string.Empty;
            else if(_searchRecipeViewModel != null)
                _searchRecipeViewModel.Query = string.Empty;
        }
    }
}
