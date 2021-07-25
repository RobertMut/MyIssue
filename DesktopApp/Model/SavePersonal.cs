using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Files;
using System.IO;
using System.Reflection;

namespace MyIssue.DesktopApp.Model
{
    public class SavePersonal
    {
        public static void Save(PersonalDetails details)
        {
            if (File.Exists(Paths.userFile)) File.Delete(Paths.userFile);
            var userDataFile = LoadFile.Load(Assembly.Load("Infrastructure").GetManifestResourceStream("MyIssue.Infrastructure.Resources.userData.xml"));
            if (File.Exists(Paths.userFile)) File.Delete(Paths.userFile);
            WriteConfiguration.WriteEmptyConfig(Paths.userFile,
                string.Format(userDataFile, details.Name, details.Surname, details.Email, details.Phone));
        }
    }
}
