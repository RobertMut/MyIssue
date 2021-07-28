using System;
using System.Globalization;
using System.Windows.Data;

namespace MyIssue.DesktopApp.Misc.Converters
{
    public class BooleanStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null ? false : (System.Convert.ToBoolean(value) ? true : false)).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return bool.Parse(value.ToString());
            } catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}
