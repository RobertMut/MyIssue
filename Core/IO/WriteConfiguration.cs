using MyIssue.Core.Interfaces;
using System;
using System.IO;

namespace MyIssue.Core.IO
{
    public class WriteConfiguration : IWriteConfig
    {
        IStringTools _tools;
        public bool WriteEmptyConfig(string path, string contents)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("IO - {0} - Configuration file does not exists!", DateTime.Now);
                _tools = new StringTools();
                byte[] fileContents = _tools.ByteMessage(contents);
                using (var fs = File.Create(path))
                {
                    fs.Write(fileContents, 0, fileContents.Length);
                }
                return true;
            }
            else
            {
                Console.WriteLine("IO - {0} - Found configuration file!", DateTime.Now);
                return false;
            }
        }

    }
}
