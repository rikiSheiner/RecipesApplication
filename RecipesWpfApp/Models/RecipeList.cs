using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    public class RecipeList : ObservableObject
    {
        private List<Recipe> _results;
        public List<Recipe> results
        {
            get { return _results; }
            set
            {
                if(_results != value) 
                {
                    _results = value;
                    OnPropertyChanged(nameof(results));
                }
            }
        }

    }
}
