using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands
{
    internal class SetAddNoteContentCommand : CommandBase
    {
        private NotesViewModel _notesViewModel;
        public SetAddNoteContentCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }
        public override void Execute(object parameter)
        {
            if (_notesViewModel.NoteToAdd == "click to add note")
                _notesViewModel.NoteToAdd = "";
            else
                _notesViewModel.NoteToAdd = "click to add note";
        }
    }
}
