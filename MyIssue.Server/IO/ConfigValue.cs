using System.Linq;
using System.Xml.Linq;
using System;

namespace MyIssue.Server.IO
{
    public class ConfigValue : IConfigValue
    {
        private readonly XDocument d;
        public ConfigValue(XDocument doc)
        {
            d = doc;
        }
        public string GetValue(string node)
        {
            Console.WriteLine(d.Descendants(node).FirstOrDefault().Value);
            //return d.Descendants(node).Select(s => s.Element(value).Value).FirstOrDefault();
            return d.Descendants(node).FirstOrDefault().Value;
        }
    }
}
