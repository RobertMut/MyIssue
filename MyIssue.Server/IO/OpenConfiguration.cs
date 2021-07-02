using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace MyIssue.Server.IO
{
    public class OpenConfiguration : IReadConfig
    {
        public XDocument OpenConfig(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return XDocument.Load(path);

                }
                else
                {
                    throw new Exception("Configuration file not found.");
                }
            } catch (Exception e)
            {
                ExceptionHandler.HandleMyException(e);
                return null;
            }

        }

    }
}
