using NUnit.Framework;
using Tests.Mocks;

namespace Tests
{
    public class ViewModelButtons
    {
        private ViewModels mocks;
        [SetUp]
        public void SetUp()
        {
            mocks = new ViewModels();
        }
        [Test]
        public void SendCommandTest()
        {
            Assert.IsNotNull(mocks.MainViewModel.Settings);
            Assert.IsFalse(mocks.MainViewModel.SendCommand.CanExecute());
            mocks.MainViewModel.Settings.CompanyName = "muspi meroL";
            mocks.MainViewModel.Description = "Lorem ipsum es dolores..";
            Assert.IsTrue(mocks.MainViewModel.SendCommand.CanExecute());
            Assert.DoesNotThrow(() => mocks.MainViewModel.SendCommand.Execute());
            
        }

    }
}
