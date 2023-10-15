using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.NotesCommands;
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

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
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

        public ICommand SetAddNoteContentCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand CancelAddNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }


        public NotesViewModel(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            RecipeId = _singleRecipeViewModel.RecipeDetails.Id;
            NoteToAdd = "click to add note";
            IsInAdding = false;

            Notes = new ObservableCollection<Note>();
            foreach (var item in _singleRecipeViewModel.RecipeDetails.Notes)
                Notes.Add(item);


            SetAddNoteContentCommand = new SetAddNoteContentCommand(this);
            AddNoteCommand = new AddNoteCommand(this);
            CancelAddNoteCommand = new CancelAddNoteCommand(this);  
            RemoveNoteCommand = new RemoveNoteCommand(this);
        }
    }
}
