using System.Globalization;
using System.Windows.Data;
using MyIssue.DesktopApp.Misc.Converters;
using NUnit.Framework;

namespace MyIssue.UnitTests
{
    class BooleanStringConverterTests
    {

        private IValueConverter _boolstr;
        [SetUp]
        public void SetUp()
        {
            _boolstr = new BooleanStringConverter();
        }
        [Test]
        public void TrueFalseConvertToStringTests()
        {
            Assert.AreEqual("False",_boolstr.Convert(false, typeof(bool), null, CultureInfo.InvariantCulture));
            Assert.AreEqual("True",_boolstr.Convert(true, typeof(bool), null, CultureInfo.InvariantCulture));
            Assert.DoesNotThrow(() => _boolstr.Convert(null, typeof(bool), null, CultureInfo.InvariantCulture));
        }
        [Test]
        public void StringToTrueFalseTests()
        {
            Assert.AreEqual(false,_boolstr.ConvertBack("False", typeof(string), null, CultureInfo.InvariantCulture));
            Assert.AreEqual(true, _boolstr.ConvertBack("True", typeof(string), null, CultureInfo.InvariantCulture));
            Assert.AreEqual(false, _boolstr.ConvertBack(null, typeof(string), null, CultureInfo.InvariantCulture));
        }
    }
}
