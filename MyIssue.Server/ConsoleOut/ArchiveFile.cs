using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace MyIssue.Server
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
                        archive.CreateEntryFromFile(Directory.GetCurrentDirectory()+@"\log.txt", "log.txt", CompressionLevel.Optimal);
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
