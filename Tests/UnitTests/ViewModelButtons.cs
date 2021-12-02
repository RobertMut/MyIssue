using MyIssue.UnitTests.Mocks;
using NUnit.Framework;

namespace MyIssue.UnitTests
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
            Assert.IsTrue(mocks.MainViewModel.SendCommand.CanExecute());
            mocks.MainViewModel.Settings.CompanyName = "muspi meroL";
            mocks.MainViewModel.Description = "Lorem ipsum es dolores..";
            Assert.IsTrue(mocks.MainViewModel.SendCommand.CanExecute());
            //Assert.DoesNotThrow(() => mocks.MainViewModel.SendCommand.Execute());
            
        }

    }
}
