using RecipesWpfApp.Commands;
using System.ComponentModel;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// משמש בשביל לייצג מודל רכיב של מתכון מתוך רשימת הרכיבים
    /// </summary>
    public class Ingredient : ObservableObject
    {
        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        /// <summary>
        /// the id of the recipe to which the ingredient belongs
        /// </summary>
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

        /// <summary>
        /// the ingredient name and amount
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public Ingredient(int index, int recipeId, string name)
        {
            Index = index;
            RecipeId = recipeId;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}