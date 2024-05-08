using RecipesWpfApp.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// משמש בשביל לייצג מודל הוראה מתוך רשימת ההוראות של מתכון
    /// </summary>
    public class Instruction : ObservableObject
    {
        /// <summary>
        /// the id of the recipe to which the instruction belongs
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
        /// the index of the instrcution in the cooking sequence
        /// </summary>
        private int _position;
        public int Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        /// <summary>
        /// the instruction content
        /// </summary>
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }


        public Instruction(int recipeId, int position, string text)
        {
            RecipeId = recipeId;
            Position = position;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}