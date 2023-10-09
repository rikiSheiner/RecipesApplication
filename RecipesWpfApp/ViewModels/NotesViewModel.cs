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

        public ICommand AddNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public NotesViewModel(ICommand addNoteCommand, ICommand removeNoteCommand)
        {
            AddNoteCommand = addNoteCommand;
            RemoveNoteCommand = removeNoteCommand;
        }
    }
}
