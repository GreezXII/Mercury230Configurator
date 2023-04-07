using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopClient.Helpers.Converters
{
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BrushConverter brushConverter = new BrushConverter();
            if ((bool)value == true)
                return brushConverter.ConvertFromString("Green") as SolidColorBrush;
            else
                return brushConverter.ConvertFromString("Red") as SolidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
