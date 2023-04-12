using System;
using System.Globalization;
using System.Windows.Data;

namespace DesktopClient.Helpers.Converters
{
    class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            return value.Equals(DateTime.MinValue) ? "" : dateTime.ToString("dd.MM.yy hh:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
