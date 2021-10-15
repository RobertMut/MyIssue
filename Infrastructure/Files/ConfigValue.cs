using System;
using System.Linq;
using System.Xml.Linq;

namespace MyIssue.Infrastructure.Files
{
    public static class ConfigValue
    {
        public static T GetValue<T>(string node, XDocument d) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType( d?.Descendants(node)?.FirstOrDefault()?.Value, typeof(T));

            } 
            catch (NullReferenceException)
            {
                return default;
            }
                
        }
    }
}
