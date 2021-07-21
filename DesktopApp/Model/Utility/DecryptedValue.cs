using System.Xml.Linq;
using MyIssue.Infrastructure.Files;

namespace MyIssue.DesktopApp.Model.Utility
{
    public class DecryptedValue
    {
        public static string GetValue(XDocument file, string value, string key = null)
        {
            if (string.IsNullOrEmpty(key)) return Crypto.AesDecrypt(ConfigValue.GetValue(value, file));
            else return Crypto.AesDecrypt(ConfigValue.GetValue(value, file), key);
        }
    }
}
