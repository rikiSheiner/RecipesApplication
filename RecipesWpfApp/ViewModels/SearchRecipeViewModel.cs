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
    /// <summary>
    /// מחלקה האחראית על הלוגיקה של עמוד חיפוש מתכונים לא שמורים
    /// </summary>
    internal class SearchRecipeViewModel : ViewModelBase
    {
        // שאילתת החיפוש של מתכונים
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

        // המתכון הנבחר מהמתכונים המוצגים
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

        // פרטי המתכון הנבחר
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

        // רשימה של כל המתכונים השמורים
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

        // האם אנו בטעינת מתכון
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        // מחלקה האחראית על הלוגיקה של תפריט הניווט
        public NavigationBarViewModel NavigationBarViewModel { get; }

        private readonly NavigationStore _navigationStore;

        public event EventHandler SelectionChangedRecipes;

        // פעולה של חיפוש מתכונים
        public ICommand SearchRecipesCommand { get; }
        // פעולה של קבלת פרטי מתכון
        public ICommand GetRecipeDetailsCommand { get; }
        // פעולה של איפוס תוכן תיבת החיפוש של מתכונים
        public ICommand SetSearchContentCommand { get; }


        public SearchRecipeViewModel(NavigationBarViewModel navigationBarViewModel, 
            NavigationStore navigationStore)
        {
            Query = "search recipe";
            NavigationBarViewModel = navigationBarViewModel;
            _navigationStore = navigationStore;
            IsLoading = false;
            //_recipes = new List<Recipe>();
            SearchRecipesCommand = new SearchRecipesCommand(this);
            GetRecipeDetailsCommand = new GetRecipeDetailsCommand(this, NavigationBarViewModel, _navigationStore);
            SetSearchContentCommand = new SetSearchContentCommand(this);
        }

    }
}
