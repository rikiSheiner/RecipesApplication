using RecipesWpfApp.Commands;
using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipesWpfApp.ViewModels
{
    internal class NotesViewModel : ViewModelBase
    {
        private int _recipeId;
        public int RecipeId
        {
            get { return _recipeId; }
            set
            {
                if (_recipeId != value)
                {
                    _recipeId = value;
                    OnPropertyChanged(nameof(RecipeId));
                }
            }
        }

        private string _noteToAdd;
        public string NoteToAdd
        {
            get { return _noteToAdd; }
            set
            { if (_noteToAdd != value)
                {
                    _noteToAdd = value;
                    OnPropertyChanged(nameof(NoteToAdd));
                }
            }
        }

        private List<Note> _notes;
        public List<Note> Notes
        {
            get { return _notes; }
            set 
            { 
                if(_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }

        private SingleRecipeViewModel _singleRecipeViewModel;

        public ICommand AddNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public NotesViewModel(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            RecipeId = _singleRecipeViewModel.RecipeDetails.Id;
            _noteToAdd = "click to add note";
            Notes = _singleRecipeViewModel.RecipeDetails.Notes;

            AddNoteCommand = new AddNoteCommand(this);
            RemoveNoteCommand = new RemoveNoteCommand(this);
        }
    }
}
