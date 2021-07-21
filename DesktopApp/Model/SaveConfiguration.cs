using MyIssue.Core.Entities;
using MyIssue.DesktopApp.Model.Utility;
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
    class SaveConfiguration
    {
        public void Save(SettingTextBoxes settings, bool smtp)
        {
            if (File.Exists(Paths.confFile)) File.Delete(Paths.confFile);
            string newImage = Paths.path + "image" + Path.GetExtension(settings.Image);
            if (smtp)
            {
                Stream emailConf = Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.configuration.xml");
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
                Stream serverConf = Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.configurationServer.xml");
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
