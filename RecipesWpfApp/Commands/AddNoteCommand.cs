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

namespace RecipesWpfApp.Commands
{
    internal class AddNoteCommand : AsyncCommandBase
    {
        private NotesViewModel _notesViewModel;
        private HttpClient client;
        private const string API_URL_NOTE = "https://localhost:7079/api/Note";

        public AddNoteCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string content = _notesViewModel.NoteToAdd;
            int recipeId = _notesViewModel.RecipeId;

            if (!string.IsNullOrWhiteSpace(content))
            {
                Note newNote = new Note(_notesViewModel.Notes.Count, recipeId, content);
                _notesViewModel.Notes.Add(newNote);
                
                // הוספת ההערה לבסיס הנתונים
                string serializedNote = JsonConvert.SerializeObject(newNote);

                HttpResponseMessage saveResponse = await client.PostAsync(API_URL_NOTE,
                     new StringContent(serializedNote, Encoding.UTF8, "application/json"));

                if (saveResponse.IsSuccessStatusCode)
                {
                    //MessageBox.Show("success");
                }
                else
                {
                    //MessageBox.Show("ERROR");
                }
            }

        }
    }
}
