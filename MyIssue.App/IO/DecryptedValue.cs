using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyIssue.DesktopApp.Cryptography;
using MyIssue.Core.Interfaces;
using MyIssue.Core.IO;

namespace MyIssue.DesktopApp.IO
{
    public class DecryptedValue : IDecryptedValue
    {
        private readonly IReadConfig _read;
        private readonly string filePath;
        private readonly XDocument xml;
        public DecryptedValue(string path)
        {
            filePath = path;
            _read = new OpenConfiguration();
            xml = _read.OpenConfig(filePath);
        }
        public string GetValue(string value, string key = null)
        {
            if (string.IsNullOrEmpty(key)) return Crypto.AesDecrypt(ConfigValue.GetValue(value, xml));
            else
            {
                return Crypto.AesDecrypt(ConfigValue.GetValue(value, xml), key);
            } 
                
        }
    }
}
