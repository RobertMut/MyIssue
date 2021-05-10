using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class StringProcessing : Tools
    {
        public override byte[] ByteMessage(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        public override string ExtractLogin(string loginInput)
        {
            //Console.WriteLine(loginInput);
            return loginInput.Remove(0, loginInput.IndexOf(' ') + 1);
        }
        public override string StringMessage(byte[] input, int length)
        {
            return Encoding.UTF8.GetString(input, 0, length).Replace("\u0000", "");
        }

    }
}
