using System.Xml.Linq;

namespace MyIssue.Core.Interfaces
{
    public interface IReadConfig
    {
        XDocument OpenConfig(string path);
    }
}
