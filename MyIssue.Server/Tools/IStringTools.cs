using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Tools
{
    public interface IStringTools
    {
        byte[] ByteMessage(string input);
        string StringMessage(byte[] input, int length);
        int? NullableInt(string input);
        string[] CommandSplitter(string input, string splitString);
        string[] SplitBrackets(string input, char firstbracket, char secondbracket);
    }
}
