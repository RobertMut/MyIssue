using System;
using System.Globalization;
using System.Windows.Data;

namespace MyIssue.DesktopApp.ViewModel.Converters
{
    class BooleanStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse((string)value, out bool r);
            return r;
        }
    }
}
