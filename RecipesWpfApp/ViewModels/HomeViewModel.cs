using RecipesWpfApp.Commands;
using RecipesWpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Services;

namespace RecipesWpfApp.ViewModels
{
    /// <summary>
    /// מחלקה המשמשת לייצוג הלוגיקה של עמוד הבית
    /// </summary>
    internal class HomeViewModel : ViewModelBase
    {
        // מופע של מחלקת הלוגיקה עבור ניווט
        public NavigationBarViewModel NavigationBarViewModel { get; }
        // מופע של פקודת מעבר לעמוד חיפוש מתכונים חדשים 
        public ICommand NavigateSearchRecipesCommand { get; set; }
        // מופע של פקודת מעבור לעמוד של המתכונים השמורים
        public ICommand NavigateRecipesBookCommand { get; set; }

        public HomeViewModel(NavigationBarViewModel navigationBarViewModel, //NavigationStore navigationStore,
            NavigationService<SearchRecipeViewModel> searchRecipeNavigationService,
            NavigationService<RecipesBookViewModel> recipesBookNavigationService)
        { 
            NavigationBarViewModel = navigationBarViewModel;
            NavigateSearchRecipesCommand = new NavigateCommand<SearchRecipeViewModel>(searchRecipeNavigationService);
            NavigateRecipesBookCommand = new NavigateCommand<RecipesBookViewModel>(recipesBookNavigationService);
        }

    }
}
