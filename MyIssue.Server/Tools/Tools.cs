using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class Tools
    {
        public virtual byte[] ByteMessage(string input)
        {
            return new byte[0];
        }

        public virtual string ExtractLogin(string loginInput)
        {
            return "Err";
        }
        public virtual string StringMessage(byte[] input, int length)
        {
            return "Err";
        }
        public virtual bool UserPass(string login, string pass)
        {
            return false;
        }
    }
}
