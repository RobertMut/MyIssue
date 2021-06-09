using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyIssue.Server.IO
{
    public interface IReadConfig
    {
        XDocument OpenConfig();
    }
}
