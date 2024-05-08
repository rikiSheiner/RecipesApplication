using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RecipesWpfApp.ViewModels
{
    /// <summary>
    /// מחלקה האחראית על הלוגיקה של דירוג כוכבים למתכון
    /// </summary>
    internal class StarRatingViewModel : ViewModelBase
    {
        private SingleRecipeViewModel _singleRecipeViewModel;

        private int _currentRating;
        public int CurrentRating
        {
            get { return _currentRating; }
            set
            {
                if (_currentRating != value)
                {
                    _currentRating = value;
                    _singleRecipeViewModel.RecipeDetails.Ranking = _currentRating;
                    OnPropertyChanged(nameof(CurrentRating));
                }
            }
        }

        private Brush _star1Fill;
        public Brush Star1Fill
        {
            get { return _star1Fill; }
            set
            {
                if(_star1Fill != value)
                {
                    _star1Fill = value;
                    OnPropertyChanged(nameof(Star1Fill));
                }
            }
        }
        private Brush _star2Fill;
        public Brush Star2Fill
        {
            get { return _star2Fill; }
            set
            {
                if (_star2Fill != value)
                {
                    _star2Fill = value;
                    OnPropertyChanged(nameof(Star2Fill));
                }
            }
        }
        private Brush _star3Fill;
        public Brush Star3Fill
        {
            get { return _star3Fill; }
            set
            {
                if (_star3Fill != value)
                {
                    _star3Fill = value;
                    OnPropertyChanged(nameof(Star3Fill));
                }
            }
        }
        private Brush _star4Fill;
        public Brush Star4Fill
        {
            get { return _star4Fill; }
            set
            {
                if (_star4Fill != value)
                {
                    _star4Fill = value;
                    OnPropertyChanged(nameof(Star4Fill));
                }
            }
        }
        private Brush _star5Fill;
        public Brush Star5Fill
        {
            get { return _star5Fill; }
            set
            {
                if (_star5Fill != value)
                {
                    _star5Fill = value;
                    OnPropertyChanged(nameof(Star5Fill));
                }
            }
        }

        public ChangeStarFillCommand ChangeStarFillCommand { get; }
        public StarRatingViewModel(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            CurrentRating = 0;
            ChangeStarFillCommand = new ChangeStarFillCommand(this);

            Star1Fill = Star2Fill = Star3Fill = Star4Fill = Star5Fill =
                new SolidColorBrush(Colors.White);
        }
    }

}
