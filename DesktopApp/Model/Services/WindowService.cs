using Microsoft.Win32;
using MyIssue.DesktopApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyIssue.DesktopApp.Model.Services
{
    public class WindowService : IWindowService
    {
        private Window prompt = null;
        private Window settings = null;
        private Window mainWindow = null;
        private OpenFileDialog fileDialog = null;
        public WindowService()
        {

        }
        public void ShowPrompt()
        {
            prompt = new Prompt();
            prompt.ShowDialog();
        }
        public void ClosePrompt()
        {
            if (!(prompt is null)) prompt.Close();
        }
        public void ShowSettings()
        {
            settings = new SettingsWindow();
            settings.ShowDialog();
        }
        public void CloseSettings()
        {
            if (!(settings is null)) settings.Close();
        }
        public void ShowMainWindow()
        {
            mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }
        public void CloseMainWindow()
        {
            if (!(mainWindow is null)) settings.Close();
        }
        public string ShowFileDialog()
        {
            fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true) return fileDialog.FileName;
            return string.Empty;
        }
    }
}
