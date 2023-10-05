using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    public class JewishHoliday : ObservableObject
    {
        public static int JewishHolidayCounter = 0;
        private int _holidayId;
        public int HolidayId
        {
            get { return _holidayId; }

            set
            {
                if (_holidayId != value)
                {
                    _holidayId = value;
                    OnPropertyChanged(nameof(HolidayId));
                }
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string _hebrewName;
        public string HebrewName
        {
            get { return _hebrewName; }

            set
            {
                if (_hebrewName != value)
                {
                    _hebrewName = value;
                    OnPropertyChanged(nameof(HebrewName));
                }
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }

            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        private string _date;
        public string Date
        {
            get { return _date; }

            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private string _hebrewDate;
        public string HebrewDate
        {
            get { return _hebrewDate; }

            set
            {
                if (_hebrewDate != value)
                {
                    _hebrewDate = value;
                    OnPropertyChanged(nameof(HebrewDate));
                }
            }
        }

        public JewishHoliday()
        {
            HolidayId = JewishHolidayCounter++;
        }
    }
}
