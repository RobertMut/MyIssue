using System.IO;

namespace MyIssue.Infrastructure.Files
{
    public class LoadFile
    {
        public static string Load(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
