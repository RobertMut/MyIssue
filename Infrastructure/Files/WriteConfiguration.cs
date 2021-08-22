using System;
using System.IO;
using MyIssue.Core.String;

namespace MyIssue.Infrastructure.Files
{
    public class WriteConfiguration
    {
        public static void WriteEmptyConfig(string path, string contents)
        {
                Console.WriteLine("IO - {0} - Configuration file does not exists!", DateTime.Now);
                byte[] fileContents = StringStatic.ByteMessage(contents);
                using (var fs = File.Create(path))
                {
                    fs.Write(fileContents, 0, fileContents.Length);
                }
        }

    }
}
