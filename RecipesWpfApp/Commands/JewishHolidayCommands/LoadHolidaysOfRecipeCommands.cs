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
using System.Collections.ObjectModel;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של טעינת כל המועדים שבהם הוכן מתכון מסוים
    /// </summary>
    internal class LoadHolidaysOfRecipeCommands : AsyncCommandBase
    {
        JewishHolidayViewModel _jewishHolidayViewModel;

        public LoadHolidaysOfRecipeCommands(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }
        
        // הפעולה מהתבצעת בעת הרצת הפקודה לטעינת מועדי המתכון
        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();

            // שליחת בקשת GET לAPI של המועדים עבור קבלת כל המועדים שבהם הוכן מתכון מסוים
            HttpResponseMessage response = await client.GetAsync(_jewishHolidayViewModel.ApiHolidayUrl + $"/{_jewishHolidayViewModel.RecipeDetails.Id}");

            if (response.IsSuccessStatusCode)
            {
                // מקבלים את רשימת המועדים מבסיס הנתונים ומעדכנים בהתאם את רשימת המועדים  
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    var holidays = JsonConvert.DeserializeObject<List<JewishHoliday>>(json);
                    _jewishHolidayViewModel.Holidays = new ObservableCollection<JewishHoliday>();

                    foreach(var holiday in holidays)
                    {
                        _jewishHolidayViewModel.Holidays.Add(holiday);
                    }
                }
            }
            else
            {
                //MessageBox.Show("ERROR");
            }
        }
    }
}
