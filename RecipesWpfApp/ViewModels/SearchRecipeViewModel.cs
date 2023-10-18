using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.RecipeCommands;
using RecipesWpfApp.Commands.RecipesListCommands;
using RecipesWpfApp.Models;
using RecipesWpfApp.Services;
using RecipesWpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Security.Policy;
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

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                if (_selectedRecipe != value)
                {
                    _selectedRecipe = value;
                    OnPropertyChanged(nameof(SelectedRecipe));
                }
            }
        }

        private RecipeDetails _selectedRecipeDetails;
        public RecipeDetails SelectedRecipeDetails
        {
            get { return _selectedRecipeDetails; }
            set
            {
                if (_selectedRecipeDetails != value)
                {
                    _selectedRecipeDetails = value;
                    OnPropertyChanged(nameof(SelectedRecipeDetails));
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

        public NavigationBarViewModel NavigationBarViewModel { get; }

        private readonly NavigationStore _navigationStore;

        public event EventHandler SelectionChangedRecipes;

        public ICommand SearchRecipesCommand { get; }
        public ICommand GetRecipeDetailsCommand { get; }


        public SearchRecipeViewModel(NavigationBarViewModel navigationBarViewModel, 
            NavigationStore navigationStore)
        {
            NavigationBarViewModel = navigationBarViewModel;
            _navigationStore = navigationStore;
            //_recipes = new List<Recipe>();
            SearchRecipesCommand = new SearchRecipesCommand(this);
            GetRecipeDetailsCommand = new GetRecipeDetailsCommand(this, NavigationBarViewModel, _navigationStore);

        }

    }
}
