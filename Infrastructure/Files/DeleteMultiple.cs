using System.IO;
using System.Linq;

namespace MyIssue.Infrastructure.Files
{
    public class DeleteMultiple
    {
        public static void DeleteFiles(string directory, string name, string extension) => Directory.GetFiles(directory,
                $"{name}.{extension}")
            .ToList().ForEach(f => File.Delete(f));
    }
}
