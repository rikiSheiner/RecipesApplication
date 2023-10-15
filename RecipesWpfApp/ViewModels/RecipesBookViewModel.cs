﻿using RecipesWpfApp.Commands;
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
    internal class RecipesBookViewModel : ViewModelBase
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

        public NavigationBarViewModel NavigationBarViewModel { get; }

        private readonly NavigationStore _navigationStore;

        public event EventHandler SelectionChangedRecipes;

        public ICommand SearchRecipesCommand { get; }
        public ICommand SelectRecipeCommand { get; }
        public ICommand LoadSavedRecipesCommand { get; }


        public RecipesBookViewModel(NavigationBarViewModel navigationBarViewModel,
            NavigationStore navigationStore)
        {
            NavigationBarViewModel = navigationBarViewModel;
            _navigationStore = navigationStore;
            _recipes = new List<RecipeDetails>();
            SearchRecipesCommand = new SearchSavedRecipesCommand(this);
            LoadSavedRecipesCommand = new LoadSavedRecipesCommand(this);

            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService =
                new ParameterNavigationService<RecipeDetails, SingleRecipeViewModel>(navigationStore,
                (parameter)=>new SingleRecipeViewModel(navigationBarViewModel,parameter, true, navigationStore));

            SelectRecipeCommand = new SelectSavedRecipeCommand(this, navigationService);

        }

    }
}
