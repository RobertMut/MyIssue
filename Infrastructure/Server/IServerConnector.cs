using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Infrastructure.Server
{
    public interface IServerConnector
    {
        string SendData(IEnumerable<byte[]> commandsToSend);
    }
}
