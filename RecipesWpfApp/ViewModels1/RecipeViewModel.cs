using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RecipesWpfApp.Models;

namespace RecipesWpfApp.ViewModels
{
    public class RecipeViewModel
    {
        private readonly HttpClient _httpClient;
        private const string BaseApiUrl = "https://localhost:7079/api";

        public RecipeViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseApiUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> SaveRecipeAsync(RecipeDetails recipe)
        {
            try
            {
                var json = JsonConvert.SerializeObject(recipe);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/Recipe", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exceptions and errors here
                return false;
            }
        }
    }

}
