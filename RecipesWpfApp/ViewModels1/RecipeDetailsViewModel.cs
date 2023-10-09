using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace RecipesWpfApp.ViewModels
{
    internal class RecipeDetailsViewModel: INotifyPropertyChanged
    {
        private RecipeDetails _recipeDetails;

        public RecipeDetails RecipeDetails
        {
            get { return _recipeDetails; }
            set
            {
                _recipeDetails = value;
                OnPropertyChanged(nameof(RecipeDetails));
            }
        }

        private int _recipeId;
        public int RecipeId
        {
            get { return _recipeId; }
            set
            {
                if( _recipeId != value )
                {
                    _recipeId = value;
                    OnPropertyChanged(nameof(RecipeId));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RecipeDetailsViewModel()
        {
        }

    }
}
