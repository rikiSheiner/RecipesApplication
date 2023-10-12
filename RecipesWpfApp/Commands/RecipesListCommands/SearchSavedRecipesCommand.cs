using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.RecipesListCommands
{
    internal class SearchSavedRecipesCommand : CommandBase
    {
        private RecipesBookViewModel recipesBookViewModel;

        public SearchSavedRecipesCommand(RecipesBookViewModel recipesBookViewModel)
        {
            this.recipesBookViewModel = recipesBookViewModel;
        }

        public override void Execute(object parameter)
        {
            string searchValue = recipesBookViewModel.Query;
            recipesBookViewModel.Recipes = recipesBookViewModel.AllRecipes.
                FindAll(recipe => recipe.Name.Contains(searchValue));

        }
    }
}
