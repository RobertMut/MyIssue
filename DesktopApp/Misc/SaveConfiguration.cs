using MyIssue.Core.Entities;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc.Utility;
using MyIssue.Infrastructure.Files;
using System.IO;
using System.Reflection;

namespace MyIssue.DesktopApp.Misc
{
    class SaveConfiguration : ISaveConfiguration
    {
        public void Save(SettingTextBoxes settings)
        {
            if (File.Exists(Paths.confFile)) File.Delete(Paths.confFile);
            string newImage = Paths.path + "image" + Path.GetExtension(settings.Image);
            if (bool.Parse(settings.ConnectionMethod))
            {
                Stream emailConf = Assembly.Load("Infrastructure").GetManifestResourceStream("MyIssue.Infrastructure.Resources.configurationDesktopSmtp.xml");
                WriteConfiguration.WriteEmptyConfig(Paths.confFile,
                    string.Format(LoadFile.Load(emailConf),
                    Crypto.AesEncrypt(settings.ApplicationPass),
                    settings.CompanyName,
                    Crypto.AesEncrypt(settings.ServerAddress, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.Port, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.Login, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.Pass, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.EmailAddress, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.RecipientAddress, settings.ApplicationPass),
                    settings.ConnectionMethod,
                    settings.SslTsl,
                    newImage
                    ));
            }

            else
            {
                Stream serverConf = Assembly.Load("Infrastructure").GetManifestResourceStream("MyIssue.Infrastructure.Resources.configurationDekstopServer.xml");
                WriteConfiguration.WriteEmptyConfig(Paths.confFile,
                    string.Format(LoadFile.Load(serverConf),
                    Crypto.AesEncrypt(settings.ApplicationPass),
                    settings.CompanyName,
                    Crypto.AesEncrypt(settings.ServerAddress, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.Port, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.Login, settings.ApplicationPass),
                    Crypto.AesEncrypt(settings.Pass, settings.ApplicationPass),
                    settings.ConnectionMethod,
                    newImage
                    ));
            }
            DeleteMultiple.DeleteFiles(Paths.path, "image", "*");
            File.Copy(settings.Image, newImage);
        }
    }
}
