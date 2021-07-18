using Microsoft.Win32;
using MyIssue.Core.Interfaces;

namespace MyIssue.DesktopApp.ViewModel
{
    public class ImageFileDialog : IImageFileDialog
    {
        public string ImageDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true) return fileDialog.FileName;
            return string.Empty;

        }
    }
}
