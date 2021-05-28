using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Tools
{
    public class StringTools : IStringTools
    {
        public byte[] ByteMessage(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        public string ExtractLogin(string loginInput)
        {
            return loginInput.Remove(0, loginInput.IndexOf(' ') + 1);
        }
        public string StringMessage(byte[] input, int length)
        {
            return Encoding.UTF8.GetString(input, 0, length).Replace("\u0000", "");
        }
        public int? NullableInt(string input)
        {
            int i;
            if (int.TryParse(input, out i)) return i;
            return null;
        }
        public string[] CommandSplitter(string input, string splitString)
        {
            return input.Split(new string[] { splitString }, StringSplitOptions.None);
        }
    }
}
