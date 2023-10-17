using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NotesCommands
{
    internal class CancelAddNoteCommand : CommandBase
    {
        private NotesViewModel _notesViewModel;

        public CancelAddNoteCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }
        public override void Execute(object parameter)
        {
            _notesViewModel.NoteToAdd = _notesViewModel.DefaultNoteText;
            _notesViewModel.IsInAdding = false;
        }
    }
}
