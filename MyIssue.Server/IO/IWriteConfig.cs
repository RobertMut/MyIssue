using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.IO
{
    public interface IWriteConfig
    {
        bool WriteEmptyConfig(string path,string configuration);
    }
}
