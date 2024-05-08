using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של הוספת מועד למתכון
    /// </summary>
    internal class AddJewishHolidayCommand : AsyncCommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        public AddJewishHolidayCommand(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }

        // הפעולה שמתבצעת עבור הוספת מועד למתכון
        public override async Task ExecuteAsync(object parameter)
        {
            if (_jewishHolidayViewModel.JewishHolidayToAdd != null)
            {
                // בדיקה האם המועד הזה קיים בבסיס הנתונים
                HttpClient client = new HttpClient();
                string apiUrl = "https://localhost:7079/api/JewishHoliday";

                bool found = false;
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    List<JewishHoliday> savedHolidays;
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        savedHolidays = JsonConvert.DeserializeObject<List<JewishHoliday>>(json);
                        for (int  i = 0;  i < savedHolidays.Count();  i++)
                        {
                            if (savedHolidays[i].Name == _jewishHolidayViewModel.JewishHolidayToAdd.Name)
                            {
                                found = true;
                                /// עושים את ההשמה הזו בשביל להשתמש במזהה של המועד השמור 
                                /// ולא במזהה חדש
                                _jewishHolidayViewModel.JewishHolidayToAdd = savedHolidays[i];
                                break; 
                            }
                        }
                        //if (savedHolidays.Contains(_jewishHolidayViewModel.JewishHolidayToAdd))
                        //  found = true;
                    }
                }


                // אם המועד הזה לא שמור נשמור אותו ברשימת המועדים
                if (!found)
                {
                    string serializedHoliday = JsonConvert.SerializeObject(_jewishHolidayViewModel.JewishHolidayToAdd);

                    HttpResponseMessage saveHolidayResponse = await client.PostAsync(apiUrl,
                         new StringContent(serializedHoliday, Encoding.UTF8, "application/json"));

                }

                // ולאחר מכן נוסיף את המועד לרשימת המועדים שבהם הוכן המתכון
                RecipeInHoliday recipeInHoliday = new RecipeInHoliday(
                    _jewishHolidayViewModel.JewishHolidayToAdd.HolidayId,
                     _jewishHolidayViewModel.RecipeDetails.Id);

                string serializedRecipeInHoliday = JsonConvert.SerializeObject(recipeInHoliday);

                string recipeInHolidayURL = "https://localhost:7079/api/RecipeInHoliday";
                HttpResponseMessage saveRecipeInHolidayResponse = await client.PostAsync(recipeInHolidayURL,
                     new StringContent(serializedRecipeInHoliday, Encoding.UTF8, "application/json"));
                
                // אם המועד שוייך למתכון בהצלחה
                if (saveRecipeInHolidayResponse.IsSuccessStatusCode)
                {
                    // הוספת המועד לרשימת המועדים המוצגים במתכון זה
                    _jewishHolidayViewModel.Holidays.Add(_jewishHolidayViewModel.JewishHolidayToAdd);
                }
            }

            // נשנה את המצב כי סיימנו להוסיף את המועד למתכון
            _jewishHolidayViewModel.IsInAddingHoliday = false;
        }

            
            
    }
}

