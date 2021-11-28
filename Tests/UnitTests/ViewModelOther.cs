using System.Threading;
using System.Windows.Controls;
using MyIssue.UnitTests.Mocks;
using NUnit.Framework;

namespace MyIssue.UnitTests
{

    class ViewModelOther
    {
        ViewModels mocks;
        [SetUp]
        public void SetUp()
        {
            mocks = new ViewModels();
        }
        [Test]
        [Apartment(ApartmentState.STA)]
        public void SettingsGetPassTest()
        {
            var pass = new PasswordBox();
            pass.Password = "pass";
            object passwordObject = pass;
            mocks.SettingsViewModel.GetPass.Execute(passwordObject);
            Assert.AreEqual(pass.Password, mocks.SettingsViewModel.Settings.Pass);
        }
        [Test]
        [Apartment(ApartmentState.STA)]
        public void SettingsGetAppPassTest()
        {
            var appPass = new PasswordBox();
            appPass.Password = "password";
            object passwordObject = appPass;
            mocks.SettingsViewModel.GetAppPass.Execute(passwordObject);
            Assert.AreEqual(appPass.Password, mocks.SettingsViewModel.Settings.ApplicationPass);
        }
        [Test]
        [Apartment(ApartmentState.STA)]
        public void PromptGetPassTest()
        {
            var pass = new PasswordBox();
            pass.Password = "pass";
            object passwordObject = pass;
            mocks.PromptViewModel.GetPass.Execute(passwordObject);
            Assert.AreEqual(pass.Password, mocks.PromptViewModel.EnteredPassword);
        }
    }
}
