using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipesWpfApp.Commands.ImagesCommands
{
    internal class UploadImageCommand : AsyncCommandBase
    {
        private ImagesViewModel _imagesViewModel;
        public UploadImageCommand(ImagesViewModel imagesViewModel)
        {
            _imagesViewModel = imagesViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();

            var image = _imagesViewModel.ImageToAdd;

            if (image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            using (var formData = new MultipartFormDataContent())
            {
                // Add other properties of the object to the form data
                formData.Add(new StringContent(image.ImageId.ToString()), "ImageId");
                formData.Add(new StringContent(image.Title), "Title");
                formData.Add(new StringContent(image.ImageName), "ImageName");
                formData.Add(new StringContent(image.RecipeId.ToString()), "RecipeID");


                // Create a stream content for the image file
                var imageContent = new StreamContent(image.ImageFile.OpenReadStream());



                // Add the image content to the form data with a specified form field name
                formData.Add(imageContent, "ImageFile", image.ImageFile.FileName);

                try
                {
                    // Send the POST request
                    var response = await client.PostAsync(_imagesViewModel.ApiImageUrl, formData);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read and return the response content
                        var responseContent = await response.Content.ReadAsStringAsync();

                        var responseImage = JsonConvert.DeserializeObject<SimpleFoodImage>(responseContent);
                        image.ImageName = responseImage.ImageName;

                        _imagesViewModel.Images.Add(image);
                        _imagesViewModel.SelectedImage = image;
                        _imagesViewModel.IsInAdding = false;
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

        
    }
}
