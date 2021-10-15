using System.Net.Mail;
using System;
using MyIssue.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MyIssue.Core.Commands;
using MyIssue.Core.String;
using MyIssue.Core.Constants;
using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.Misc.Utility
{
    public class MessageConstructor : IMessageConstructor
    {
        public MailMessage BuildMessage(string subject, string recipient, string sender, PersonalDetails details, string description)
        {
            var newsubj = $"[Issue][{details.Company}][{details.Name}][{details.Surname}][{subject}]";
            var formatted =
                $"{details.Name}\r\n{details.Surname}\r\n{details.Company}\r\n{details.Phone}\r\n{details.Email}\r\n{description}";
            return new MailMessage(sender, recipient, newsubj, formatted);
        }
        public IEnumerable<byte[]> BuildTaskCommands(SettingTextBoxes settings, string description)
        {
            string commandString =
                $"{StringStatic.CutString(description)}\r\n<NEXT>\r\n{description}\r\n<NEXT>\r\n{settings.CompanyName}\r\n<NEXT>\r\n" +
                $"{"null"}\r\n<NEXT>\r\n{"null"}\r\n<NEXT>\r\n{"Normal"}\r\n<NEXT>\r\n" +
                $"{null}\r\n<NEXT>\r\n{null}\r\n<NEXT>\r\n" +
                $"{"null"}\r\n<EOF>\r\n";
            var login = User.Login(settings.Login, settings.Pass);
            return new List<byte[]>().Concat(login)
                .Append(StringStatic.ByteMessage("CreateTask\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(commandString))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));

        }
    }
}

