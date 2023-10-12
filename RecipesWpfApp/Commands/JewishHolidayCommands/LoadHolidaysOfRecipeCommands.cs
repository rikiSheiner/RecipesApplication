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
    internal class LoadHolidaysOfRecipeCommands : AsyncCommandBase
    {
        JewishHolidayViewModel _jewishHolidayViewModel;

        public LoadHolidaysOfRecipeCommands(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();


            HttpResponseMessage response = await client.GetAsync(_jewishHolidayViewModel.ApiHolidayUrl + $"?recipeId={_jewishHolidayViewModel.RecipeDetails.Id}");

            if (response.IsSuccessStatusCode)
            {
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
