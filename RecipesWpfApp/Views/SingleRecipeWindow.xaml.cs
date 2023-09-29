using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using System.Collections.ObjectModel;
//using MyProject;

namespace RecipesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ViewRecipeWindow.xaml
    /// </summary>
    public partial class SingleRecipeWindow : Window
    {
        private RecipeDetails recipeDetails;

        private ObservableCollection<Note> notes = new ObservableCollection<Note>();

        // בחלון זה יוצג מתכון מסוים על סמך הבחירה שהייתה בחלון של חיפוש מתכונים
        // תיהיה אפשרות לשמור את המתכון בתוך הספר כולל שמירת דרוג של המתכון
        // ישמרו גם תמונות של המאכל 
        //וגם ישמר תיעוד של המועדים שבהם המתכון הוכן

        public SingleRecipeWindow(RecipeDetails rd, bool isSaved=false)
        {
            InitializeComponent();
            this.recipeDetails = rd;
            this.DataContext = recipeDetails;

            starsRating.Value = recipeDetails.Ranking;

            foreach (Note note in recipeDetails.Notes)
            {
                notes.Add(note);
            }
            notesLV.ItemsSource = notes;

            if (recipeDetails != null && recipeDetails.Cook_Time_Minutes == null )
            {
                cookTimeValueTB.Visibility = Visibility.Collapsed;
                cookTimeTB.Visibility = Visibility.Collapsed;
            }


            if (isSaved)
            {
                saveBtn.Visibility = Visibility.Collapsed;

            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var URL_TO_ASP_API_ENDPOINT = "https://localhost:7079/api/Recipe";

            string serializedRecipe = JsonConvert.SerializeObject(recipeDetails);

            HttpResponseMessage saveResponse =  await client.PostAsync(URL_TO_ASP_API_ENDPOINT, new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

            if (saveResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }


        private async void AddNote_Click(object sender, RoutedEventArgs e)
        {
            string itemName = noteTextBox.Text;

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                notes.Add(new Note(notes.Count, recipeDetails.Id, itemName));

                noteTextBox.Clear();

                int id = recipeDetails.Id;

                List<Note> notesList = new List<Note>();
                foreach (Note note in notes)
                {
                    notesList.Add(note);
                }

                recipeDetails.Notes = notesList;

                // עדכון של המתכון השמור בבסיס הנתונים
                HttpClient client = new HttpClient();
                var URL_TO_ASP_API_ENDPOINT = $"https://localhost:7079/api/Recipe/{id}";
                recipeDetails.Ranking = starsRating.Value;


                string serializedRecipe = JsonConvert.SerializeObject(recipeDetails);

                HttpResponseMessage saveResponse = await client.PutAsync(URL_TO_ASP_API_ENDPOINT, new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

                if (saveResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("ERROR");
                }
            }
        }

    }
}