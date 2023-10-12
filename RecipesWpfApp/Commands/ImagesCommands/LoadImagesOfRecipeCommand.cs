using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.ComponentModel;


namespace RecipesWpfApp.Commands.ImagesCommands
{
    internal class LoadImagesOfRecipeCommand : AsyncCommandBase
    {
        private ImagesViewModel _imagesViewModel;
        
        public LoadImagesOfRecipeCommand(ImagesViewModel imagesViewModel)
        {
            _imagesViewModel = imagesViewModel;
        }

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
    }
}
