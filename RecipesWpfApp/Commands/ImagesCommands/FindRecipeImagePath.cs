using Newtonsoft.Json;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.ImagesCommands
{
    /*internal class FindRecipeImagePath : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_imagesViewModel.ApiImageUrl + $"?recipeId={_imagesViewModel.RecipeDetails.Id}");

            if (response.IsSuccessStatusCode)
            {
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    _imagesViewModel.Images = JsonConvert.DeserializeObject<List<FoodImage>>(json);
                }
            }
            else
            {
                //MessageBox.Show("ERROR");
            }
        }
    }*/
}
