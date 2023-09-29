using Newtonsoft.Json;
using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for RecipesBookWindow.xaml
    /// </summary>
    public partial class RecipesBookWindow : Window
    {
        // בחלון זה תיהיה אפשרות לחפש מתכון מהמתכונים השמורים בספר
        // לעדכן הערות
        // ולמצוא מתכונים דומים למתכונים השמורים

        private List<RecipeDetails> allRecipes;

        public RecipesBookWindow()
        {
            InitializeComponent();
            LoadRecipes(); 
        }
        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchValue = SearchTextBox.Text;
            var partialRecipes = allRecipes.FindAll(recipe=>recipe.Name.Contains(searchValue));

            ItemsDataGrid.ItemsSource = partialRecipes;
        }

        

        private async void LoadRecipes()
        {
            HttpClient client = new HttpClient();
            var URL_TO_ASP_API_ENDPOINT = "https://localhost:7079/api/Recipe";


            HttpResponseMessage response = await client.GetAsync(URL_TO_ASP_API_ENDPOINT);

            if (response.IsSuccessStatusCode)
            {
                List<RecipeDetails> recipes;
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    recipes = JsonConvert.DeserializeObject<List<RecipeDetails>>(json);
                    ItemsDataGrid.ItemsSource = recipes;
                    allRecipes = recipes;
                }
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecipeDetails selectedRecipe = ItemsDataGrid.SelectedItem as RecipeDetails;
            new SingleRecipeWindow(selectedRecipe, isSaved:true).Show();

        }
    }
}
