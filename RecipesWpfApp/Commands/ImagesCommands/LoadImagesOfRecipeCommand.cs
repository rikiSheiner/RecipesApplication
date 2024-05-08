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
    /// <summary>
    /// מחלקה המייצגת פקודה של טעינת כל התמונות השמורות עבור מתכון מסוים
    /// טעינת התמונות מתבצעת בצורה אסינכרונית ולא מעכבת את הצגת המתכון
    /// </summary>
    internal class LoadImagesOfRecipeCommand : AsyncCommandBase
    {
        private ImagesViewModel _imagesViewModel;
        
        public LoadImagesOfRecipeCommand(ImagesViewModel imagesViewModel)
        {
            _imagesViewModel = imagesViewModel;
        }

        // פעולה המתבצעת בעת הרצת  הפקודה לטעינת תמונות של מתכון מסוים  
        public override async Task ExecuteAsync(object parameter)
        {
            // יוצרים לקוח HTTP
            HttpClient client = new HttpClient();

            // ושולחים בקשת GET לAPI של התמונות
            HttpResponseMessage response = await client.GetAsync(_imagesViewModel.ApiImageUrl + $"?recipeId={_imagesViewModel.RecipeDetails.Id}");

            if (response.IsSuccessStatusCode)
            {
                // קוראים את התגובה וממירים אותה לרשימה של מתכונים
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    _imagesViewModel.Images = JsonConvert.DeserializeObject<ObservableCollection<FoodImage>>(json);


                    if (_imagesViewModel.Images.Count > 0)
                    {
                        _imagesViewModel.SelectedImage = _imagesViewModel.Images[0];
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
