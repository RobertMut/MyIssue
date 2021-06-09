using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using MyIssue.Server.Tools;

namespace MyIssue.Server.IO
{
    class WriteConfiguration : IWriteConfig
    {
        IStringTools _tools;
        public bool WriteEmptyConfig()
        {
            if (!File.Exists("configuration.xml"))
            {
                Console.WriteLine("IO - {0} - Configuration file does not exists!", DateTime.Now);
                _tools = new StringTools();
                byte[] fileContents = _tools.ByteMessage(Config.emptyConfig);
                using (var fs = File.Create("configuration.xml"))
                {
                    fs.Write(fileContents, 0, fileContents.Length);
                }
                return true;
            } else
            {
                Console.WriteLine("IO - {0} - Found configuration file!", DateTime.Now);
                return false;
            }
        }

    }
}
