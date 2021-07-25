using System;
using System.Globalization;
using System.Windows.Data;

namespace MyIssue.DesktopApp.Misc.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value) return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value) return true;
            return false;
        }
    }
}
