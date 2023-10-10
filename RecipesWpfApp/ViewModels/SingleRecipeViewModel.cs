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
        private bool _isSaved;
        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                if (_isSaved != value)
                {
                    _isSaved = value;
                    OnPropertyChanged(nameof(IsSaved));
                }
            }
        }

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

        private NotesViewModel _notesViewModel;

        public ICommand RateRecipeCommand;
        public ICommand UpdateRecipeDetailsCommand;
        public ICommand LoadSingleRecipeCommand;


        public SingleRecipeViewModel(RecipeDetails recipeDetails, bool isSaved)
        {
            RecipeDetails = recipeDetails;
            IsSaved = isSaved;

            _notesViewModel = new NotesViewModel(this);
            //LoadSingleRecipeCommand = new LoadSingleRecipeCommand(this);
        }
    }
}
