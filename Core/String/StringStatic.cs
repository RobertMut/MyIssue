using System;
using System.Text;

namespace MyIssue.Core.String
{
    public static class StringStatic
    {
        public static byte[] ByteMessage(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        public static string StringMessage(byte[] input, int length)
        {
            return Encoding.UTF8.GetString(input, 0, length).Replace("\u0000", "");
        }
        public static string[] CommandSplitter(string input, string splitString)
        {
            return input.Split(new string[] { splitString }, StringSplitOptions.None);
        }
        public static string CutString(string input)
        {
            return input.Substring(0, Math.Min(input.Length / 3, 15));
        }
    }
}
