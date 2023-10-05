using Newtonsoft.Json;
using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for AddHolidayToRecipeWindow.xaml
    /// </summary>
    public partial class AddHolidayToRecipeWindow : Window
    {
        private RecipeDetails recipe;
        public AddHolidayToRecipeWindow(RecipeDetails recipeDetails)
        {
            InitializeComponent();
            this.recipe = recipeDetails;
        }

        private async void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            // אם רוצים לתעד מתי הוכן המתכון ישנן 2 אפשרויות
            // אפשרות אחת היא לשמור את המועד שבו הוכן המתכון

            // כשאר רוצים לשמור לפי מועד נבצע זאת באופן הבא:
            // ראשית נבדוק בטבלת המועדים האם קיים המועד הזה ,
            // אם כן נשתמש במזהה של המועד הזה ונשמור את המזהה בטבלה של קשר בין מועד למתכון 
            // בטבלת הקשר בין מועד למתכון נשמור שתי שדות - מזהה מועד ומזהה מתכון
            // אחרת נוסיף רשומה חדשה לטבלת המועדים ואז נשתמש במזהה וכו


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

                JewishHoliday jewishHoliday = new JewishHoliday();

                foreach (var item in jsonObject["items"])
                {
                    if (item["title"] == moedTB.Text)
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

                searchResultTB.Text = jewishHoliday.ToString();


                // שמירה של המועד בבסיס הנתונים
                // השמירה תתבצע רק אם המועד לא קיים בבסיס הנתונים

                // בדיקה האם המתכון קיים בבסיס הנתונים
                string API_URL = "https://localhost:7079/api/Recipe/holiday";

                var holiday = await client.GetAsync(API_URL + $"/one?name={jewishHoliday.Name}");
                if (holiday.IsSuccessStatusCode)
                {

                }
                else // שומרים את המועד רק אם הוא לא קיים בבסיס הנתונים
                {
                    string serializedHoliday = JsonConvert.SerializeObject(jewishHoliday);


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


                // שמירת תיעוד של הכנת המתכון במועד מסוים
                var content = JsonConvert.SerializeObject(new RecipeInHoliday(jewishHoliday.HolidayId, recipe.Id));
                var response3 = await client.PostAsync("https://localhost:7079/api/Recipe/recipe-in-holiday", new StringContent(content, Encoding.UTF8, "application/json"));

                if (response3.IsSuccessStatusCode)
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("ERROR");
                }
            }
        }
    }
}
