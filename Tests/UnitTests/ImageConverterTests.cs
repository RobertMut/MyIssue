using System;
using System.Globalization;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MyIssue.DesktopApp.Misc.Converters;
using NUnit.Framework;

namespace MyIssue.UnitTests
{
    public class ImageConverterTests
    {
        private ImageConverter converter;
        private string testImg = Directory.GetFiles(@"C:\Windows\Web\Screen", "*.jpg")[0];
        [SetUp]
        public void SetUp()
        {
            converter = new ImageConverter();
        }
        [Test]
        public void ImageConverterTest()
        {
            
            
            Assert.AreSame(typeof(BitmapImage), converter.Convert(testImg, typeof(ImageSource), null, CultureInfo.InvariantCulture).GetType());
            Assert.Throws<InvalidOperationException>(() => converter.Convert(testImg, typeof(string), null, CultureInfo.InvariantCulture).GetType());
        }
        [Test]
        public void ImageConvertBackTest()
        {
            var img = converter.Convert(testImg, typeof(ImageSource), null, CultureInfo.InvariantCulture);
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(img, typeof(string), null, CultureInfo.InvariantCulture));
            
        }
    }
}
