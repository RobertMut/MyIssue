using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Infrastructure.Files
{
    public class DeleteMultiple
    {
        public static void DeleteFiles(string directory, string name, string extension) => Directory.GetFiles(directory, string.Format("{0}.{1}", name, extension))
            .ToList().ForEach(f => File.Delete(f));
    }
}
