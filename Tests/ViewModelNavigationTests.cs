using Moq;
using MyIssue.DesktopApp.ViewModel;
using MyIssue.DesktopApp.Views;
using NUnit.Framework;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;
using Unity;

namespace Tests
{
    class ViewModelNavigationTests
    {
        ViewModels mock;
        [SetUp]
        public void SetUp()
        {
            mock = new ViewModels();
        }
        [Test]
        public void MainWindowViewNavigate()
        {
            Assert.IsNotNull(mock.MainWindowViewModel.LoadMainCommand);
            Assert.IsTrue(mock.MainWindowViewModel.LoadMainCommand.CanExecute());
            Assert.DoesNotThrow(() =>
            {
                mock.MainWindowViewModel.LoadMainCommand.Execute();
            });

        }
        [Test]
        public void MainViewNavigate()
        {
            Assert.IsNotNull(mock.MainViewModel.EditSettings);
            Assert.IsTrue(mock.MainViewModel.EditSettings.CanExecute());
            Assert.DoesNotThrow(() =>
            {
                mock.MainViewModel.EditSettings.Execute();
            });
        }
        [Test]
        public void PromptViewNavigate()
        {
            Assert.IsNotNull(mock.PromptViewModel.OpenSettings);
            Assert.IsFalse(mock.PromptViewModel.OpenSettings.CanExecute());
            Assert.DoesNotThrow(() =>
            {
                mock.PromptViewModel.OpenSettings.Execute();
            });
            Assert.IsNotNull(mock.PromptViewModel.ReturnToMainView);
            Assert.IsTrue(mock.PromptViewModel.ReturnToMainView.CanExecute());
            Assert.DoesNotThrow(() =>
            {
                mock.PromptViewModel.ReturnToMainView.Execute();
            });
        }
        [Test]
        public void SettingsViewNavigate()
        {
            Assert.IsNotNull(mock.SettingsViewModel.ReturnToMain);
            Assert.IsTrue(mock.SettingsViewModel.ReturnToMain.CanExecute());
            Assert.DoesNotThrow(() => { mock.SettingsViewModel.ReturnToMain.Execute(); });
        }
    }
}