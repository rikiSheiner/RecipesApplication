using RecipesWpfApp.ViewModels;
using RecipesWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.RecipeCommands
{
    internal class RateRecipeCommand : CommandBase
    {
        private SingleRecipeViewModel _singleRecipeViewModel;
        public RateRecipeCommand(SingleRecipeViewModel singleRecipeViewModel )
        {
            _singleRecipeViewModel = singleRecipeViewModel;
        }
        public override void Execute(object parameter)
        {
            
        }
    }
}
