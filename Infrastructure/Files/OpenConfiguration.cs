using System;
using System.IO;
using System.Xml.Linq;
using MyIssue.Core.Exceptions;

namespace MyIssue.Infrastructure.Files
{
    public class OpenConfiguration
    {
        public static XDocument OpenConfig(string path)
        {
            try
            {
                if (!File.Exists(path)) throw new ConfigurationNotFoundException("Configuration file not found.");
                    return XDocument.Load(path);
            }
            catch (ConfigurationNotFoundException e)
            {
                ExceptionHandler.HandleMyException(e);
                return null;
            }

        }

    }
}
