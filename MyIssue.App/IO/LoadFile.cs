using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.App.IO
{
    public class LoadFile
    {
        public static string Load(string path)
        {
            using(StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
        public static string Load(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
