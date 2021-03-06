using MyIssue.Core.String;
using System.Collections.Generic;
using System.Linq;

namespace MyIssue.Server
{
    public class SplitToCommand
    {
        public static string[] Get(IEnumerable<string> input)
        {
            return StringStatic.CommandSplitter(input.ToList()[input.Count() - 1], "\r\n<NEXT>\r\n");
        } 
    }
}
