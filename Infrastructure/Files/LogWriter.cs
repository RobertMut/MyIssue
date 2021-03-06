using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using MyIssue.Core.Interfaces;

namespace MyIssue.Infrastructure.Files
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
                    SerilogLogger.Log(value);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString()); ;
                    SerilogLogger.ServerLogException(e);
                }
            }

        }
    }

}
