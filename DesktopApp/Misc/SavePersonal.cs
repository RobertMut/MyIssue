using MyIssue.Infrastructure.Files;
using System.IO;
using System.Reflection;
using MyIssue.DesktopApp.Entities;

namespace MyIssue.DesktopApp.Misc
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
