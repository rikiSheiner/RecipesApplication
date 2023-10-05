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
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
//using MyProject;

namespace RecipesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ViewRecipeWindow.xaml
    /// </summary>
    public partial class SingleRecipeWindow : Window
    {
        private HttpClient client;
        private const string API_URL = "https://localhost:7079/api/Recipe";
        private RecipeDetails recipeDetails;
        private FoodImage myFoodImage;
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
            client = new HttpClient();

            starsRating.Value = recipeDetails.Ranking;

            foreach (Note note in recipeDetails.Notes)
            {
                notes.Add(note);
            }
            notesLV.ItemsSource = notes;

            if (recipeDetails != null)
            {
                if(recipeDetails.Cook_Time_Minutes == null || recipeDetails.Cook_Time_Minutes.Value <= 0)
                {
                    cookTimeValueTB.Visibility = Visibility.Collapsed;
                    cookTimeTB.Visibility = Visibility.Collapsed;
                }
            }


            if (isSaved)
            {
                saveBtn.Visibility = Visibility.Collapsed;

                // show the saved image if exists
                ShowFoodImage();
            }
        }

        private async void ShowFoodImage()
        {
            var response = await client.GetAsync(API_URL+ $"/image?recipeId={recipeDetails.Id}");

            if (response.IsSuccessStatusCode)
            {
                try
                {

                    string responseContent = await response.Content.ReadAsStringAsync();

                    myFoodImage = JsonConvert.DeserializeObject<FoodImage>(responseContent);

                    dynamic jsonObject = JsonConvert.DeserializeObject(responseContent);

                    string path = Path.Combine("C://Users//1//Source//Repos//MyFinalProject2023//MyProject//Images//", jsonObject["imageName"].ToString());

                    recipeImage.Source = new BitmapImage(new Uri(path));

                    //UploadButton.Visibility = Visibility.Collapsed;
                    //BrowseButton.Visibility = Visibility.Collapsed;


                    //recipeDetails.Images.Add(myFoodImage.ImageFile);
                }
                catch (Exception ex) { }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string serializedRecipe = JsonConvert.SerializeObject(recipeDetails);

            HttpResponseMessage saveResponse =  await client.PostAsync(API_URL, new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

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
                recipeDetails.Ranking = starsRating.Value;


                string serializedRecipe = JsonConvert.SerializeObject(recipeDetails);

                HttpResponseMessage saveResponse = await client.PutAsync(API_URL+$"/{id}", new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

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


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif|All files (*.*)|*.*",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Create the RecipeImageObject and set its properties based on the selected file
                myFoodImage = new FoodImage
                {
                    Title = recipeDetails.Name, // Set other properties as needed
                    ImageName = recipeDetails.Name + " Image",
                    RecipeId = recipeDetails.Id,
                    ImageFile = new FormFile(openFileDialog.OpenFile(), 0, openFileDialog.OpenFile().Length, recipeDetails.Name+" Image", openFileDialog.FileName)
                };

                string imagePath = openFileDialog.FileName;

                recipeImage.Source = new BitmapImage(new Uri(imagePath));


                //recipeDetails.Images.Add(myFoodImage.ImageFile);



                

            }
        }

        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (myFoodImage == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            using (var formData = new MultipartFormDataContent())
            {
                // Add other properties of the object to the form data
                formData.Add(new StringContent(myFoodImage.ImageId.ToString()), "ImageId");
                formData.Add(new StringContent(myFoodImage.Title), "Title");
                formData.Add(new StringContent(myFoodImage.ImageName), "ImageName");
                formData.Add(new StringContent(myFoodImage.RecipeId.ToString()), "RecipeID");


                // Create a stream content for the image file
                var imageContent = new StreamContent(myFoodImage.ImageFile.OpenReadStream());



                // Add the image content to the form data with a specified form field name
                formData.Add(imageContent, "ImageFile", myFoodImage.ImageFile.FileName);

                try
                {
                    // Send the POST request
                    var response = await client.PostAsync(API_URL + "/image", formData);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read and return the response content
                        var responseContent = await response.Content.ReadAsStringAsync();
                        //return responseContent;
                    }
                    else
                    {
                        // Handle the error
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the upload
                    throw new Exception($"Error: {ex.Message}");
                }
            }
        }

        private void moedBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddHolidayToRecipeWindow(recipeDetails).Show();
            this.Close();
        }
    }
}
