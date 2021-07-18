using System;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using System.IO;
using System.Linq;
using System.IO.Compression;

namespace MyIssue.Infrastructure.Files
{
    public class ArchiveFile : IArchiveFile
    {
        public void ArchiveLog()
        {
            if (File.Exists("log.txt"))
            {
                try
                {
                    int lastLog = GetNewLogIndex(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip"));
                    string zipname = Directory.GetCurrentDirectory()+@"\"+lastLog.ToString() + ".zip";
                    using (FileStream zip = new FileStream(zipname, FileMode.Create))
                    using (ZipArchive archive = new ZipArchive(zip, ZipArchiveMode.Create, true))
                    {
                        archive.CreateEntryFromFile(Directory.GetCurrentDirectory() + @"\log.txt",
                                                    "log.txt",
                                                    CompressionLevel.Optimal);
                    }
                }
                catch (Exception e)
                {
                    ExceptionHandler.HandleMyException(e);
                }
                File.Create("log.txt").Dispose();
            }
            else
            {
                File.Create("log.txt").Dispose();
            }
        }
        private int GetNewLogIndex(string[] files)
        {
            string number = files.Select(element =>
            {
                return Path.GetFileName(element).Replace(".zip", "");
            }
            ).ToList().Max();
            Int32.TryParse(number, out var a);
            ++a;
            return a;
        }
    }
}
