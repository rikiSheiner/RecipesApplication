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

namespace RecipesWpfApp.ViewModels
{
    internal class ImagesViewModel : ViewModelBase
    {
        private ObservableCollection<IFormFile> _images;
        public ObservableCollection<IFormFile> Images
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

        public string ApiImageUrl { get; }

        private SingleRecipeViewModel _singleRecipeViewModel;

        public ICommand AddImageCommand;
        public ICommand RemoveImageCommand;

        public ICommand BrowseImageCommand;
        public ICommand UploadImageCommand;

        public ImagesViewModel(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            RecipeDetails = _singleRecipeViewModel.RecipeDetails;
            ApiImageUrl = "https://localhost:7079/api/Image";

            Images = new ObservableCollection<IFormFile>();

            BrowseImageCommand = new BrowseImageCommand(this);
            UploadImageCommand = new UploadImageCommand(this);

        }
    }
}
