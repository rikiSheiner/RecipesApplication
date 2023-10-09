using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.ViewModels
{
    internal class IngredientsViewModel : ViewModelBase
    {
        private List<Ingredient> _ingredients;
        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set
            {
                if(_ingredients != value)
                {
                    _ingredients = value;
                    OnPropertyChanged(nameof(Ingredients));
                }
            }
        }

        public IngredientsViewModel()
        {
                
        }
    }
}
