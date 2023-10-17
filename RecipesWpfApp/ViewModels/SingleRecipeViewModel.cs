﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.RecipeCommands;
using RecipesWpfApp.Models;
using RecipesWpfApp.Stores;

namespace RecipesWpfApp.ViewModels
{
    internal class SingleRecipeViewModel : ViewModelBase
    {
        private bool _isNotSaved;
        public bool IsNotSaved
        {
            get { return _isNotSaved; }
            set
            {
                if (_isNotSaved != value)
                {
                    _isNotSaved = value;
                    IsSaved = !_isNotSaved;
                    OnPropertyChanged(nameof(IsNotSaved));
                }
            }
        }

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
        public NotesViewModel NotesViewModel
        {
            get { return _notesViewModel; }
            set
            {
                if(_notesViewModel != value)
                {
                    _notesViewModel = value;
                    OnPropertyChanged(nameof(NotesViewModel));
                }
            }
        }

        private ImagesViewModel _imagesViewModel;
        public ImagesViewModel ImagesViewModel
        {
            get { return _imagesViewModel; }
            set
            {
                if (_imagesViewModel != value)
                {
                    _imagesViewModel = value;
                    OnPropertyChanged(nameof(ImagesViewModel));
                }
            }
        }

        private JewishHolidayViewModel _jewishHolidayViewModel;
        private RecipeDetails parameter;
        private NavigationStore navigationStore;

        public JewishHolidayViewModel JewishHolidayViewModel
        {
            get { return _jewishHolidayViewModel; }
            set
            {
                if(_jewishHolidayViewModel != value)
                {
                    _jewishHolidayViewModel = value;
                    OnPropertyChanged(nameof(JewishHolidayViewModel));
                }
            }
        }

        public ICommand SaveRecipeCommand { get; }
        public ICommand RateRecipeCommand { get; }
        public ICommand UpdateRecipeDetailsCommand { get; }

        private readonly NavigationStore _navigationStore;
        public NavigationBarViewModel NavigationBarViewModel { get; }

        public SingleRecipeViewModel(NavigationBarViewModel navigationBarViewModel,
            RecipeDetails recipeDetails, bool isSaved,
            NavigationStore navigationStore)
        {
            NavigationBarViewModel = navigationBarViewModel;
            RecipeDetails = recipeDetails;
            IsNotSaved = !isSaved;
            IsSaved = isSaved;

            SaveRecipeCommand = new SaveRecipeCommand(this);
            UpdateRecipeDetailsCommand = new UpdateRecipeDetailsCommand(this);

            _navigationStore = navigationStore;

            _notesViewModel = new NotesViewModel(this);
            _imagesViewModel = new ImagesViewModel(this);
            _jewishHolidayViewModel = new JewishHolidayViewModel(this, _navigationStore);

        }

    }
}
