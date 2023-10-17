using Microsoft.AspNetCore.Http;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.ImagesCommands;
using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RecipesWpfApp.ViewModels
{
    internal class ImagesViewModel : ViewModelBase
    {
        // The list of food images objects which belongs to specific recipe
        private List<FoodImage> _images;
        public List<FoodImage> Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }


        // The image of the recipe to add
        private FoodImage _imageToAdd;
        public FoodImage ImageToAdd
        {
            get { return _imageToAdd; }
            set
            {
                if (_imageToAdd != value)
                {
                    _imageToAdd = value;
                    OnPropertyChanged(nameof(ImageToAdd));
                }
            }
        }


        // The selected image to be shown in big size
        private FoodImage _selectedImage;
        public FoodImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }

        private BitmapImage _bitmapImageToAdd;
        public BitmapImage BitmapImageToAdd
        {
            get { return _bitmapImageToAdd; }
            set
            {
                if(value != _bitmapImageToAdd)
                {
                    _bitmapImageToAdd = value;
                    OnPropertyChanged(nameof(BitmapImageToAdd));
                }
            }
        }

        // The details of the selected recipe
        private RecipeDetails _recipeDetails;
        public RecipeDetails RecipeDetails
        {
            get { return _recipeDetails; }
            set
            {
                if (_recipeDetails != value)
                {
                    _recipeDetails = value;
                    OnPropertyChanged(nameof(RecipeDetails));
                }
            }
        }

        private bool _isInAdding;
        public bool IsInAdding
        {
            get { return _isInAdding; }
            set
            {
                if (_isInAdding != value)
                {
                    _isInAdding = value;
                    OnPropertyChanged(nameof(IsInAdding));
                }
            }
        }

        public string IconCameraPath { get; }

        // The URL of the api of the images 
        public string ApiImageUrl { get; }

        // The view model of the current recipe
        private SingleRecipeViewModel _singleRecipeViewModel;

        public ICommand LoadImagesOfRecipeCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }

        public ICommand BrowseImageCommand { get; }
        public ICommand UploadImageCommand { get; }

        public ImagesViewModel(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            RecipeDetails = _singleRecipeViewModel.RecipeDetails;
            ApiImageUrl = "https://localhost:7079/api/FoodImage";

            IsInAdding = false;
            IconCameraPath = "C:/Users/1/Source/Repos/MyFinalProject2023/MyProject/BackgroundImages/icon-camera.png";

            LoadImagesOfRecipeCommand = new LoadImagesOfRecipeCommand(this);
            BrowseImageCommand = new BrowseImageCommand(this);
            UploadImageCommand = new UploadImageCommand(this);

        }
    }
}
