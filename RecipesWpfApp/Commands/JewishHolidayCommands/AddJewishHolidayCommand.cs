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

namespace RecipesWpfApp.Commands.JewishHolidayCommands
{
    internal class AddJewishHolidayCommand : AsyncCommandBase
    {
        private JewishHolidayViewModel _jewishHolidayViewModel;
        public AddJewishHolidayCommand(JewishHolidayViewModel jewishHolidayViewModel)
        {
            _jewishHolidayViewModel = jewishHolidayViewModel;
        }

        
        public override async Task ExecuteAsync(object parameter)
        {
            if (_jewishHolidayViewModel.JewishHolidayToAdd != null)
            {
                // בשורות הקוד הבאות מתבצעת יצירת אוביקט של מועד ישראל
                // על סמך הקלט של המועד שהזין המשתמש
                // לאחר יצירת אוביקט המועד נצטרך לבצע שמירה של המועד בבסיס הנתונים


                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://www.hebcal.com/holidays/")
                };

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

                    JewishHoliday jewishHoliday = _jewishHolidayViewModel.JewishHolidayToAdd;

                    foreach (var item in jsonObject["items"])
                    {
                        if (item["title"] == jewishHoliday.Name)
                        {
                            jewishHoliday = new JewishHoliday
                            {
                                Name = item["title"],
                                HebrewName = item["hebrew"],
                                Date = item["date"],
                                HebrewDate = item["hdate"],
                                Description = item["memo"]
                            };
                            break;
                        }
                    }


                    // שמירה של המועד בבסיס הנתונים

                    string serializedHoliday = JsonConvert.SerializeObject(jewishHoliday);

                    string API_URL = "https://localhost:7079/api/JewishHoliday";

                    HttpResponseMessage saveResponse = await client.PostAsync(API_URL, new StringContent(serializedHoliday, Encoding.UTF8, "application/json"));

                    if (saveResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("success");
                    }
                    else
                    {
                        MessageBox.Show("ERROR");
                    }
                }

                _jewishHolidayViewModel.IsInAddingHoliday = false;
            }

            
            
        }
    }
}
