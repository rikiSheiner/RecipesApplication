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
                Brush prevStarBrush;
                Brush currBrush;
                switch(starIndex)
                {
                    case 1:
                        prevStarBrush = _starRatingViewModel.Star1Fill;
                        currBrush = ChangeBrush(prevStarBrush);
                        if(((SolidColorBrush)currBrush).Color == Colors.White)
                        {
                            FillAllStarsWithWhite();
                            break;
                        }
                        _starRatingViewModel.Star1Fill = currBrush;
                        break;
                    case 2:
                        prevStarBrush = _starRatingViewModel.Star2Fill;
                        currBrush = ChangeBrush(prevStarBrush);
                        if (((SolidColorBrush)currBrush).Color == Colors.White)
                        {
                            FillAllStarsWithWhite();
                            break;
                        }
                        _starRatingViewModel.Star1Fill = currBrush;
                        _starRatingViewModel.Star2Fill = currBrush;
                        break;
                    case 3:
                        prevStarBrush = _starRatingViewModel.Star3Fill;
                        currBrush = ChangeBrush(prevStarBrush);
                        if (((SolidColorBrush)currBrush).Color == Colors.White)
                        {
                            FillAllStarsWithWhite();
                            break;
                        }
                        _starRatingViewModel.Star1Fill = currBrush;
                        _starRatingViewModel.Star2Fill = currBrush;
                        _starRatingViewModel.Star3Fill = currBrush;
                        break;
                    case 4:
                        prevStarBrush = _starRatingViewModel.Star4Fill;
                        currBrush = ChangeBrush(prevStarBrush);
                        if (((SolidColorBrush)currBrush).Color == Colors.White)
                        {
                            FillAllStarsWithWhite();
                            break;
                        }
                        _starRatingViewModel.Star1Fill = currBrush;
                        _starRatingViewModel.Star2Fill = currBrush;
                        _starRatingViewModel.Star3Fill = currBrush;
                        _starRatingViewModel.Star4Fill = currBrush;
                        break;
                    case 5:
                        prevStarBrush = _starRatingViewModel.Star5Fill;
                        currBrush = ChangeBrush(prevStarBrush);
                        if (((SolidColorBrush)currBrush).Color == Colors.White)
                        {
                            FillAllStarsWithWhite();
                            break;
                        }
                        _starRatingViewModel.Star1Fill = currBrush;
                        _starRatingViewModel.Star2Fill = currBrush;
                        _starRatingViewModel.Star3Fill = currBrush;
                        _starRatingViewModel.Star4Fill = currBrush;
                        _starRatingViewModel.Star5Fill = currBrush;
                        break;
                    default:
                        break;
                }
            }
        }

        private SolidColorBrush ChangeBrush(Brush prevBrush)
        {
            if (prevBrush is SolidColorBrush solidColorBrush)
            {
                int ratingVal = _starRatingViewModel.CurrentRating;

                if (solidColorBrush.Color == Colors.White)
                {
                    _starRatingViewModel.CurrentRating = ratingVal + 1;
                    return new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    _starRatingViewModel.CurrentRating = ratingVal - 1;
                    return new SolidColorBrush(Colors.White);
                }
            }

            return new SolidColorBrush(Colors.White);
        }

        private void FillAllStarsWithWhite()
        {
            var white = new SolidColorBrush(Colors.White);
            _starRatingViewModel.Star1Fill = white;
            _starRatingViewModel.Star2Fill = white;
            _starRatingViewModel.Star3Fill = white;
            _starRatingViewModel.Star4Fill = white;
            _starRatingViewModel.Star5Fill = white;

        }
    }
}
