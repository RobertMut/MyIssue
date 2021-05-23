using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public static class LogWriter
    {
        private static TextWriter _writer;
        public static void Init(IArchiveFile _archiveFile)
        {
            _writer = Console.Out;
            Console.SetOut(new ConsoleOut());
            _archiveFile.ArchiveLog();
        }
        private class ConsoleOut : TextWriter
        {
            public override Encoding Encoding { get; }
            public override void WriteLine(string value)
            {
                try
                {
                    _writer.WriteLine(value);
                    using(StreamWriter w = File.AppendText("log.txt"))
                    {
                        w.WriteLine(value);
                        w.Close();
                    }
                    //File.AppendText("log.txt").WriteLine(value);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString()); ;
                    ExceptionHandler.HandleMyException(e);
                }
            }

        }
    }

}
