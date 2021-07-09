using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.App.IO
{
    public interface IConfigurationBuilder
    {
         IConfigurationBuilder SetApplicationPass(string input);
         IConfigurationBuilder SetCompanyName(string input);
         IConfigurationBuilder SetServerAddress(string input);
         IConfigurationBuilder SetPort(string input);
         IConfigurationBuilder SetLogin(string input);
         IConfigurationBuilder SetPass(string input);
         IConfigurationBuilder SetEmailAddress(string input);
         IConfigurationBuilder SetRecipientAddress(string input);
         IConfigurationBuilder SetConnectionMethod(bool input);
         IConfigurationBuilder SetSslTsl(bool input);
         ConfigValues Build();
    }
}
