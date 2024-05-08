using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של טעינת כל המועדים שבהם הוכן מתכון מסויים
    /// </summary>
    internal class LoadAllHolidaysCommand : AsyncCommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        public LoadAllHolidaysCommand(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }

        // הפעולה שמתבצעת בעת הרצת הפקודה לטעינת המועדים של מתכון מסוים
        public override async Task ExecuteAsync(object parameter)
        {
            // קבלת כל המועדים השמורים בבסיס הנתונים ע"י הAPI
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://www.hebcal.com/holidays/")
            };

            // שליחת בקשת HTTP לREST API של המועדים
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://www.hebcal.com/hebcal?cfg=json&v=1&maj=on&min=off&mod=off&nx=off&year=now&month=x&ss=off&mf=off&c=off&geo=none")
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                dynamic jsonObject = JsonConvert.DeserializeObject(responseContent);

                // שמירת כל המועדים הקיימים באוביקט המתאים
                _jewishHolidayViewModel.AllHolidays = new ObservableCollection<JewishHoliday>();

                foreach (var item in jsonObject["items"])
                {
                    var currentHoliday = new JewishHoliday
                    {
                        Name = item["title"],
                        HebrewName = item["hebrew"],
                        Date = item["date"],
                        HebrewDate = item["hdate"],
                        Description = item["memo"]
                    };
                    _jewishHolidayViewModel.AllHolidays.Add(currentHoliday);
                }
            }
        }
    }
}
