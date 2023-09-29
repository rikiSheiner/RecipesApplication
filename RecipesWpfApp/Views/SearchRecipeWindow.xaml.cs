using Newtonsoft.Json;
using RecipesWpfApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace RecipesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for SearchRecipeWindow.xaml
    /// </summary>
    public partial class SearchRecipeWindow : Window
    {
        // בחלון זה תיהיה אפשרות לחפש מתכונים על בסיס מילות מפתח מתוך המאגר של המתכונים
        // או על בסיס מרכיבי תפריט
        // וכן תיהיה אפשרות לעבור לחלון של מתכון ספציפי
        const string BASE_URL = "https://tasty.p.rapidapi.com/";
        const string LIST_ENDPOINT = "recipes/list?from=0&size=20&tags=under_30_minutes&q={0}";
        const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
        const string API_HOST = "tasty.p.rapidapi.com";

        private readonly HttpClient _httpClient;

        public SearchRecipeWindow()
        {
            InitializeComponent();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL)
            };

        }
        private async void LoadItems(string query)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BASE_URL + string.Format(LIST_ENDPOINT, query)),
                Headers =
                {
                    { "X-RapidAPI-Key", API_KEY },
                    { "X-RapidAPI-Host", API_HOST },
                },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                RecipeList items = JsonConvert.DeserializeObject<RecipeList>(responseContent);

                ItemsDataGrid.ItemsSource = items.results;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;
            LoadItems(searchQuery);
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe selectedItem = (Recipe)ItemsDataGrid.SelectedItem;
            if(selectedItem != null)
            {
                int recipeId = selectedItem.Id;

                new LoadSingleRecipe(recipeId);

            }
        }
    }
}
