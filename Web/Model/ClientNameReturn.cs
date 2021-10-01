using System.Collections.Generic;

namespace MyIssue.Web.Model
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
