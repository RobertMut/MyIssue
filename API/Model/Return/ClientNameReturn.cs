using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.API.Model.Return
{
    public class ClientNameRoot
    {
        public List<ClientNameReturn> Clients { get; set; }
    }
    public class ClientNameReturn
    {
        public string CompanyName { get; set; }
    }
}
