using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Win32;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RecipesWpfApp.Commands.ImagesCommands
{
    internal class BrowseImageCommand : CommandBase
    {
        private ImagesViewModel _imagesViewModel;

        public BrowseImageCommand(ImagesViewModel imagesViewModel)
        {
            _imagesViewModel = imagesViewModel;
        }
        public override void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif|All files (*.*)|*.*",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _imagesViewModel.ImageToAdd = new FoodImage
                {
                    Title = _imagesViewModel.RecipeDetails.Name, 
                    ImageName = _imagesViewModel.RecipeDetails.Name + " Image",
                    RecipeId = _imagesViewModel.RecipeDetails.Id,
                    ImageFile = new FormFile(openFileDialog.OpenFile(), 0, openFileDialog.OpenFile().Length, _imagesViewModel.RecipeDetails.Name + " Image", openFileDialog.FileName)
                };

                string imagePath = openFileDialog.FileName;

                _imagesViewModel.BitmapImageToAdd = new BitmapImage(new Uri(imagePath));

                _imagesViewModel.IsInAdding = true;
            }
        }


    }
}
