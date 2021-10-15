using System.Collections.Generic;

namespace MyIssue.Core.Model.Return
{
    public class ClientReturnRoot
    {
        public List<ClientReturn> Clients { get; set; }
    }
    public class ClientReturn
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string No { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string FlatNo { get; set; }
        public string Description { get; set; }
    }
}
