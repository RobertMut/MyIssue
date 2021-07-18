using MyIssue.Core.Entities;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Model;
using MyIssue.Infrastructure.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.DesktopApp.ViewModel
{
    public class TextBoxesToConfiguration : ITextBoxesToConfiguration
    {
        public void DoWriting(bool isSmtp, SettingTextBoxes textBoxes)
        {
            if (isSmtp)
            {
                Stream emailConf = Assembly.GetExecutingAssembly().GetManifestResourceStream("Infrastructure.Resources.configurationDesktopSmtp.xml");
                WriteConfiguration.WriteEmptyConfig(Paths.confFile,
                    string.Format(LoadFile.Load(emailConf),
                    Crypto.AesEncrypt(textBoxes.ApplicationPass),
                    textBoxes.CompanyName,

                    Crypto.AesEncrypt(textBoxes.ServerAddress, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.Port, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.Login, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.Pass, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.EmailAddress, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.RecipientAddress, textBoxes.ApplicationPass),
                    isSmtp.ToString(),
                    textBoxes.SslTsl.ToString(),
                    textBoxes.Image
                    ));
            }
            else
            {
                Stream serverConf = Assembly.GetExecutingAssembly().GetManifestResourceStream("Infrastructure.Resources.configurationDesktopServer.xml");
                WriteConfiguration.WriteEmptyConfig(Paths.confFile,
                    string.Format(LoadFile.Load(serverConf),
                    Crypto.AesEncrypt(textBoxes.ApplicationPass),
                    textBoxes.CompanyName,
                    Crypto.AesEncrypt(textBoxes.ServerAddress, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.Port, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.Login, textBoxes.ApplicationPass),
                    Crypto.AesEncrypt(textBoxes.Pass, textBoxes.ApplicationPass),
                    isSmtp.ToString(),
                    textBoxes.Image
                    ));
            }
        }
    }
}
