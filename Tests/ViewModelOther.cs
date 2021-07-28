using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tests.Mocks;

namespace Tests
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
