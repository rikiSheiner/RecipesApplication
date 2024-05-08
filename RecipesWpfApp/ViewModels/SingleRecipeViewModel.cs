using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.RecipeCommands;
using RecipesWpfApp.Models;
using RecipesWpfApp.Stores;

namespace RecipesWpfApp.ViewModels
{
    /// <summary>
    /// מחלקה האחראית על הלוגיקה של עמוד חיפוש מתכון יחיד
    /// </summary>
    internal class SingleRecipeViewModel : ViewModelBase
    {
        // האם המתכון לא שמור
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

        // האם המתכון שמור
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

        // פרטי המתכון הנוכחי
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

        // מופע המחלקה האחראית על הלוגיקה של הערות המתכון
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

        // מחלקה האחראית על הלוגיקה של תמונות המתכון
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

        // מופע מחלקה האחראית על הלוגיקה של המועדים שבהם הוכן המתכון
        private JewishHolidayViewModel _jewishHolidayViewModel;
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

        // מופע מחלקה האחראית על הלוגיקה של דירוג המתכון
        private StarRatingViewModel _starRatingViewModel;
        public StarRatingViewModel StarRatingViewModel
        {
            get { return _starRatingViewModel; }
            set
            {
                if(_starRatingViewModel != value)
                {
                    _starRatingViewModel = value;
                    OnPropertyChanged(nameof(StarRatingViewModel)); 
                }
            }
        }


        //private RecipeDetails parameter;
        //private NavigationStore navigationStore;

        // פעולה של שמירת המתכון
        public ICommand SaveRecipeCommand { get; }
        // פעולה של דירוג המתכון
        public ICommand RateRecipeCommand { get; }
        // פעולה של עדכון פרטי מתכון
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
            RateRecipeCommand = new UpdateRecipeDetailsCommand(this);

            _navigationStore = navigationStore;

            _notesViewModel = new NotesViewModel(this);
            _imagesViewModel = new ImagesViewModel(this);
            _jewishHolidayViewModel = new JewishHolidayViewModel(this, _navigationStore);
            _starRatingViewModel = new StarRatingViewModel(this);

            UpdateStarsColors();
        }

        private void UpdateStarsColors()
        {
            if (_starRatingViewModel != null)
            {
                var yellow = new SolidColorBrush(Colors.Yellow);
                switch(RecipeDetails.Ranking)
                {
                    case 5:
                        _starRatingViewModel.Star5Fill = yellow;
                        goto case 4;

                    case 4:
                        _starRatingViewModel.Star4Fill = yellow;
                        goto case 3;

                    case 3:
                        _starRatingViewModel.Star3Fill = yellow;
                        goto case 2;

                    case 2:
                        _starRatingViewModel.Star2Fill = yellow;
                        goto case 1;

                    case 1:
                        _starRatingViewModel.Star1Fill = yellow;
                        break;
                }

                _starRatingViewModel.CurrentRating = RecipeDetails.Ranking;
            }
        }

    }
}
