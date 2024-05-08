using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.RecipeCommands;
using RecipesWpfApp.Commands.RecipesListCommands;
using RecipesWpfApp.Models;
using RecipesWpfApp.Services;
using RecipesWpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipesWpfApp.ViewModels
{
    /// <summary>
    /// מחלקה האחראית על הלוגיקה הקשורה לתצוגת המתכונים שמורים
    /// </summary>
    internal class RecipesBookViewModel : ViewModelBase
    {
        // שאילתת החיפוש עבור תיבת חיפוש מתכונים שמורים
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

        // המתכון הנבחר מבין המתכונים השמורים 
        private RecipeDetails _selectedRecipe;
        public RecipeDetails SelectedRecipe
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

        // רשימה המכילה את פרטי כל המתכונים השמורים
        private List<RecipeDetails> _recipes;
        public List<RecipeDetails> Recipes
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

        // רשימה המכילה את פרטי כל המתכונים השמורים
        private List<RecipeDetails> _allRecipes;
        public List<RecipeDetails> AllRecipes
        {
            get { return _allRecipes; }
            set
            {
                if(_allRecipes != value)
                {
                    _allRecipes = value;
                    OnPropertyChanged(nameof(AllRecipes));  
                }
            }
        }

        // האם אנו עדיין בטעינת המתכונים השמורים או שכבר סיימנו
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

        // מחלקה האחראית על הלוגיגה של תפריט הניווט
        public NavigationBarViewModel NavigationBarViewModel { get; }

        private readonly NavigationStore _navigationStore;
        
        // אירוע של שינוי מתכון נבחר
        public event EventHandler SelectionChangedRecipes;

        // פעולה של חיפוש מתכונים
        public ICommand SearchRecipesCommand { get; }
        // פעולה של בחירת מתכון
        public ICommand SelectRecipeCommand { get; }
       // פעולןה של טעינת כל המתכונים השמורים
        public ICommand LoadSavedRecipesCommand { get; }
        // פעולה של איפוס תיבת חיפוש מתכונים
        public ICommand SetSearchContentCommand { get; }


        public RecipesBookViewModel(NavigationBarViewModel navigationBarViewModel,
            NavigationStore navigationStore)
        {
            Query = "search recipe";
            NavigationBarViewModel = navigationBarViewModel;
            _navigationStore = navigationStore;
            IsLoading = false;
            //_recipes = new List<RecipeDetails>();
            SearchRecipesCommand = new SearchSavedRecipesCommand(this);
            LoadSavedRecipesCommand = new LoadSavedRecipesCommand(this);

            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService =
                new ParameterNavigationService<RecipeDetails, SingleRecipeViewModel>(navigationStore,
                (parameter)=>new SingleRecipeViewModel(navigationBarViewModel,parameter, true, navigationStore));

            SelectRecipeCommand = new SelectSavedRecipeCommand(this, navigationService);
            SetSearchContentCommand = new SetSearchContentCommand(this);
        }

    }
}
