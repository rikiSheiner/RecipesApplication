using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RecipesWpfApp.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isFilled && targetType == typeof(Brush))
            {
                if (isFilled)
                {
                    // Return a filled brush (e.g., yellow)
                    return new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    // Return an unfilled brush (e.g., transparent)
                    return new SolidColorBrush(Colors.Transparent);
                }
            }

            // Return a default brush (e.g., transparent) if the conversion fails
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
