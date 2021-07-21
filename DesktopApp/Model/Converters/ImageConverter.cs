using System;
using System.Globalization;
using MyIssue.Core.Exceptions;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MyIssue.DesktopApp.ViewModel.Converters
{
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource)) throw new InvaidOperationException("Target type must be System.Windows.Media.ImageSource.");
            try
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri((string)value, UriKind.Relative);
                img.EndInit();
                return img;
            } catch(Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
