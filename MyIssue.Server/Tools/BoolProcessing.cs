using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class BoolProcessing : Tools
    {

        public override bool UserPass(string login, string pass)
        {
            if (String.Equals(login, "admin") && String.Equals(pass, "1234"))
                return true;
            else
                return false;

        }
    }
}
