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
    internal class AddJewishHolidayCommand : AsyncCommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        public AddJewishHolidayCommand(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }

        
        public override async Task ExecuteAsync(object parameter)
        {
            if (_jewishHolidayViewModel.JewishHolidayToAdd != null)
            {
                // Check if the holiday exists in the database
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
                        if (savedHolidays.Contains(_jewishHolidayViewModel.JewishHolidayToAdd))
                            found = true;
                    }
                }

                // If not, save the holiday in the database
                if (!found)
                {
                    string serializedHoliday = JsonConvert.SerializeObject(_jewishHolidayViewModel.JewishHolidayToAdd);

                    HttpResponseMessage saveHolidayResponse = await client.PostAsync(apiUrl,
                         new StringContent(serializedHoliday, Encoding.UTF8, "application/json"));

                }

                // Save documentation of holiday when it is recommended to cook the recipe
                RecipeInHoliday recipeInHoliday = new RecipeInHoliday(
                    _jewishHolidayViewModel.JewishHolidayToAdd.HolidayId,
                     _jewishHolidayViewModel.RecipeDetails.Id);

                string serializedRecipeInHoliday = JsonConvert.SerializeObject(recipeInHoliday);

                HttpResponseMessage saveRecipeInHolidayResponse = await client.PostAsync(apiUrl,
                     new StringContent(serializedRecipeInHoliday, Encoding.UTF8, "application/json"));
            }

            _jewishHolidayViewModel.IsInAddingHoliday = false;
        }

            
            
    }
}

