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
                if (File.Exists(path))
                {
                    return XDocument.Load(path);

                }
                else
                {
                    throw new ConfigurationNotFoundException("Configuration file not found.");
                }
            }
            catch (ConfigurationNotFoundException e)
            {
                ExceptionHandler.HandleMyException(e);
                return null;
            }

        }

    }
}
