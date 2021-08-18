using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MyIssue.Infrastructure.Files
{
    public static class ConfigValue
    {
        public static T GetValue<T>(string node, XDocument d) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType( d?.Descendants(node).FirstOrDefault().Value , typeof(T));

            } 
            catch (NullReferenceException)
            {
                return default;
            }
                
        }
    }
}
