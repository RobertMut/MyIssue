using MyIssue.Infrastructure.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyIssue.DesktopApp.ViewModel
{
    public class SaveMessageBox : ISaveMessageBox
    {
        public void Box(string name, string surname, string email, string phone)
        {
            MessageBoxResult saveMessage = MessageBox.Show("Do you want to save your personal details?", "Save details",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (saveMessage.Equals(MessageBoxResult.Yes))
            {
                var userData = LoadFile.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.userData.xml"));
                string formatted = string.Format(userData,
                    name,
                    surname,
                    email,
                    phone
                    );
                if (File.Exists(Paths.userFile)) File.Delete(Paths.userFile);
                WriteConfiguration.WriteEmptyConfig(Paths.userFile, formatted);
            }
        }
    }
}
