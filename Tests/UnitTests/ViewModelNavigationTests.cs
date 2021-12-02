using MyIssue.UnitTests.Mocks;
using NUnit.Framework;

namespace MyIssue.UnitTests
{
    public class ViewModelNavigationTests
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
            Assert.IsNotNull(mock.MainWindowViewModel.LoadLogoCommand);
            Assert.IsTrue(mock.MainWindowViewModel.LoadLogoCommand.CanExecute());
            Assert.DoesNotThrow(() =>
            {
                mock.MainWindowViewModel.LoadLogoCommand.Execute();
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
            Assert.IsTrue(mock.PromptViewModel.OpenSettings.CanExecute());
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