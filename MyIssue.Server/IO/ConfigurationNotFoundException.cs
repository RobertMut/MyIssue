using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.IO
{
    class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException() : base()
        {

        }
        public ConfigurationNotFoundException(string message) : base(message)
        {

        }
        public ConfigurationNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
