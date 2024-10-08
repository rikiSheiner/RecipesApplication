﻿using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipesWpfApp.Commands.NotesCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה להוספת הערה עבור מתכון שמור
    /// </summary>
    internal class AddNoteCommand : AsyncCommandBase
    {
        private NotesViewModel _notesViewModel;
        private HttpClient client;
        private const string API_URL_NOTE = "https://localhost:7079/api/Note";

        public AddNoteCommand(NotesViewModel notesViewModel)
        {
            client = new HttpClient()
            {
                BaseAddress=new Uri(API_URL_NOTE)
            };

            _notesViewModel = notesViewModel;
        }

        // הפעולה המתבצעת בעת הרצת הפקודה להוספת הערה למתכון
        public override async Task ExecuteAsync(object parameter)
        {
            // חילוץ פרטי ההערה להוספה
            string content = _notesViewModel.NoteToAdd;
            int recipeId = _notesViewModel.RecipeId;

            if (!string.IsNullOrWhiteSpace(content))
            {
                // יצירת ההערה החדשה של המתכון
                Note newNote = new Note(_notesViewModel.Notes.Count, recipeId, content);
                // הוספת ההערה לרשימת ההערות עבור עדכון התצוגה
                _notesViewModel.Notes.Add(newNote);
                
                // הוספת ההערה לבסיס הנתונים
                string serializedNote = JsonConvert.SerializeObject(newNote);

                HttpResponseMessage saveResponse = await client.PostAsync(API_URL_NOTE,
                     new StringContent(serializedNote, Encoding.UTF8, "application/json"));

                if (saveResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("note added successfuly");
                }
                else
                {
                    MessageBox.Show("ERROR");
                }
            }

            // עדכונים נדרשים עבור סיום הוספת הערה
            _notesViewModel.NoteToAdd = _notesViewModel.DefaultNoteText;
            _notesViewModel.IsInAdding = false;

        }
    }
}
