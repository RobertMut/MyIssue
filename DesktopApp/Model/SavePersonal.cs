using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.DesktopApp.Model
{
    public class SavePersonal
    {
        public static void Save(PersonalDetails details)
        {
            if (File.Exists(Paths.userFile)) File.Delete(Paths.userFile);
            var userDataFile = LoadFile.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.userData.xml"));
            if (File.Exists(Paths.userFile)) File.Delete(Paths.userFile);
            WriteConfiguration.WriteEmptyConfig(Paths.userFile,
                string.Format(userDataFile, details.Name, details.Surname, details.Email, details.Phone));
        }
    }
}
