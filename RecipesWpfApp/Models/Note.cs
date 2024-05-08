using RecipesWpfApp.Commands;
using System.ComponentModel;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// משמש בשביל לייצג הערה מתוך רשימת ההערות שנשמרו עבור מאכל מסוים
    /// </summary>
    public class Note : ObservableObject
    {
        private int _noteNumber;
        public int NoteNumber
        {
            get { return _noteNumber; }

            set
            {
                if (_noteNumber != value)
                {
                    _noteNumber = value;
                    OnPropertyChanged("NoteNumber");
                }
            }
        }

        /// <summary>
        /// the id of the recipe to which the note belongs
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
        /// the content of the note
        /// </summary>
        private string _content;
        public string Content
        {
            get { return _content; }

            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged("Content");
                }
            }
        }

        public Note(int noteNumber, int recipeId, string content)
        {
            NoteNumber = noteNumber;
            RecipeId = recipeId;
            Content = content;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}