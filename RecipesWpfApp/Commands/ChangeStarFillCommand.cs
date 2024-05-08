using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace RecipesWpfApp.Commands
{
    internal class ChangeStarFillCommand : CommandBase
    {
        private StarRatingViewModel _starRatingViewModel;
        public ChangeStarFillCommand(StarRatingViewModel starRatingViewModel)
        {
            _starRatingViewModel = starRatingViewModel;
        }
        public override void Execute(object parameter)
        {
            int starIndex;
            bool isInt = int.TryParse(parameter as string, out starIndex);
            if(isInt)
            {
                _starRatingViewModel.CurrentRating = starIndex;

                Brush yellowBrush = new SolidColorBrush(Colors.Yellow);
                Brush whiteBrush = new SolidColorBrush(Colors.White);

                switch(starIndex)
                {
                    case 1:
                        _starRatingViewModel.Star1Fill = yellowBrush;
                        _starRatingViewModel.Star2Fill = whiteBrush;
                        _starRatingViewModel.Star3Fill = whiteBrush;
                        _starRatingViewModel.Star4Fill = whiteBrush;
                        _starRatingViewModel.Star5Fill = whiteBrush;
                        break;

                    case 2:
                        _starRatingViewModel.Star1Fill = yellowBrush;
                        _starRatingViewModel.Star2Fill = yellowBrush;
                        _starRatingViewModel.Star3Fill = whiteBrush;
                        _starRatingViewModel.Star4Fill = whiteBrush;
                        _starRatingViewModel.Star5Fill = whiteBrush;
                        
                        break;
                    case 3:
                        _starRatingViewModel.Star1Fill = yellowBrush;
                        _starRatingViewModel.Star2Fill = yellowBrush;
                        _starRatingViewModel.Star3Fill = yellowBrush;
                        _starRatingViewModel.Star4Fill = whiteBrush;
                        _starRatingViewModel.Star5Fill = whiteBrush;
                        break;
                    case 4:
                        _starRatingViewModel.Star1Fill = yellowBrush;
                        _starRatingViewModel.Star2Fill = yellowBrush;
                        _starRatingViewModel.Star3Fill = yellowBrush;
                        _starRatingViewModel.Star4Fill = yellowBrush;
                        _starRatingViewModel.Star5Fill = whiteBrush;
                        break;
                    case 5:
                        _starRatingViewModel.Star1Fill = yellowBrush;
                        _starRatingViewModel.Star2Fill = yellowBrush;
                        _starRatingViewModel.Star3Fill = yellowBrush;
                        _starRatingViewModel.Star4Fill = yellowBrush;
                        _starRatingViewModel.Star5Fill = yellowBrush;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
