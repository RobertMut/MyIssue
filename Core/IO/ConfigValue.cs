using System.Linq;
using System.Xml.Linq;

namespace MyIssue.Core.IO
{
    public static class ConfigValue
    {
        public static string GetValue(string node, XDocument d)
        {
            return d.Descendants(node).FirstOrDefault().Value;
        }
    }
}
