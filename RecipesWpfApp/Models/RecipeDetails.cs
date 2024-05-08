using Microsoft.AspNetCore.Http;
using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;
using Newtonsoft.Json;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// משמש בשביל לייצג מודל של מתכון מפורט
    /// כולל את כל הפרטים הרלוונטיים השייכים למתכון זה
    /// מחלקה זו מכילה טיפול במקרה של מתכון שמור ומתכון לא שמור 
    /// </summary>
    public class RecipeDetails : ObservableObject
    {
        private int _id;
        public int Id
        {
            get { return _id; }

            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }


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
        
        private string _description;
        public string Description
        {
            get { return _description; }

            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Descreption");
                }
            }
        }
        private List<Ingredient> _ingredients;
        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }

            set
            {
                if (_ingredients != value)
                {
                    _ingredients = value;
                    OnPropertyChanged("Ingredients");
                }
            }
        }

        private List<Instruction> _instructions;
        public List<Instruction> Instructions
        {
            get { return _instructions; }

            set
            {
                if (_instructions != value)
                {
                    _instructions = value;
                    OnPropertyChanged("Instructions");
                }
            }
        }

        private int? _cook_time_minutes;
        public int? Cook_Time_Minutes
        {
            get { return _cook_time_minutes; }

            set
            {
                if (_cook_time_minutes != value)
                {
                    _cook_time_minutes = value;
                    OnPropertyChanged("Cook_Time_Minutes");
                }
            }
        }

        private int _num_serving;
        public int Num_Servings
        {
            get { return _num_serving; }

            set
            {
                if (_num_serving != value)
                {
                    _num_serving = value;
                    OnPropertyChanged("Num_Servings");
                }
            }
        }

        private int _ranking;
        public int Ranking
        {
            get { return _ranking; }

            set
            {
                if (_ranking != value)
                {
                    _ranking = value;
                    OnPropertyChanged("Ranking");
                }
            }
        }

        private List<Note> _notes;
        public List<Note> Notes
        {
            get { return _notes; }

            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        /*private List<JewishHolidayToAdd> _jewishHolidays;
        public List<JewishHolidayToAdd> JewishHolidays
        {
            get { return _jewishHolidays; }
            set
            {
                if (_jewishHolidays != value)
                {
                    _jewishHolidays = value;
                    OnPropertyChanged("JewishHolidays");
                }
            }
        }*/
        /*
        private List<IFormFile> _images;
        public List<IFormFile> Images
        {
            get { return _images; }

            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }
        */

        private List<FoodImage> _foodImage;
        public List<FoodImage> FoodImage
        {
            get { return _foodImage; }
            set
            {
                if(_foodImage != value)
                {
                    _foodImage = value;
                    OnPropertyChanged(nameof(FoodImage));
                }
            }
        }

        private string _imagePath;
        [JsonIgnore]
        public string ImagePath
        {
            get { return _imagePath; }

            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }

        public RecipeDetails()
        {
            _notes = new List<Note>();
            _instructions = new List<Instruction>();
            _ingredients = new List<Ingredient>();
            _foodImage = new List<FoodImage>();

            ImagePath = "C:\\Users\\1\\Source\\Repos\\MyFinalProject2023\\RecipesWpfApp\\Icons\\meal-icon.png";
            //_jewishHolidays = new List<JewishHolidayToAdd> ();
        }
    }
}
