using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// this class reperesents the documentation of jewish holidays 
    /// when the recipe was in use
    /// </summary>
    internal class RecipeInHoliday : ObservableObject
    {
        private int _holidayId;
        public int HolidayId
        {
            get { return _holidayId; }

            set
            {
                if (_holidayId != value)
                {
                    _holidayId = value;
                    OnPropertyChanged("HolidayId");
                }
            }
        }
        private int _recipeId;
        public int RecipeId
        {
            get { return _recipeId; }

            set
            {
                if (_recipeId != value)
                {
                    _recipeId = value;
                    OnPropertyChanged("RecipeId");
                }
            }
        }

        public RecipeInHoliday(int holidayId, int recipeId)
        {
            HolidayId = holidayId;
            RecipeId = recipeId;
        }

    }
}
