using RecipesWpfApp.Commands;
using RecipesWpfApp.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace RecipesWpfApp.ViewModels
{
    internal class SearchRecipeViewModel : ViewModelBase
    {
        private string _query;
        public string Query
        {
            get { return _query; }
            set
            {
                if (_query != value)
                {
                    _query = value;
                    OnPropertyChanged(nameof(Query));
                }
            }
        }

        private List<Recipe> _recipes;
        public List<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                if (_recipes != value)
                {
                    _recipes = value;
                    OnPropertyChanged(nameof(Recipes));
                }
            }
        }

        public ICommand SearchRecipesCommand { get; }
        public ICommand SelectCommand { get; }

        /*public SearchRecipeViewModel(ICommand searchCommand, ICommand selectCommand)
        {
            _recipes = new List<Recipe>();
            SearchRecipesCommand = searchCommand;
            SelectCommand = selectCommand;
        }*/

        public SearchRecipeViewModel()
        {
            _recipes = new List<Recipe>();
            SearchRecipesCommand = new SearchRecipesCommand(this);
        }
    }
}
