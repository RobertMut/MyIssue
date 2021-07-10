using MyIssue.Core.Interfaces;
using System;
using System.Text;

namespace MyIssue.Core.String
{
    public class StringTools : IStringTools
    {
        public byte[] ByteMessage(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        public string StringMessage(byte[] input, int length)
        {
            return Encoding.UTF8.GetString(input, 0, length).Replace("\u0000", "");
        }
        public int? NullableInt(string input)
        {
            if (int.TryParse(input, out int i)) return i;
            return null;
        }
        public string[] CommandSplitter(string input, string splitString)
        {
            return input.Split(new string[] { splitString }, StringSplitOptions.None);
        }
        public string[] SplitBrackets(string input, char firstbracket, char secondbracket)
        {
            return input.Split(firstbracket, secondbracket);
        }
    }
}
