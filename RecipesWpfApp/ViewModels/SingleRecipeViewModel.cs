using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Models;

namespace RecipesWpfApp.ViewModels
{
    internal class SingleRecipeViewModel : ViewModelBase
    {
        private RecipeDetails _recipeDetails;
        public RecipeDetails RecipeDetails
        {
            get { return _recipeDetails; }
            set
            {
                if (_recipeDetails != value)
                {
                    _recipeDetails = value;
                    OnPropertyChanged(nameof(_recipeDetails));
                }
            }
        }

        public ICommand RateRecipeCommand;
        public ICommand UpdateRecipeDetailsCommand;


        public SingleRecipeViewModel(ICommand rateCommand,ICommand updateRecipeCommand)
        {
            RateRecipeCommand = rateCommand;
            UpdateRecipeDetailsCommand = updateRecipeCommand;
        }
    }
}
