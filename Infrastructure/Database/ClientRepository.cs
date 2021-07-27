using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Infrastructure.Database
{
    public class ClientRepository : Repository<CLIENT>, IClientRepository
    {
        public ClientRepository(MyIssueDatabase context) : base(context)
        {

        }
        public Decimal? GetClientByName(string name)
        {
            return _context?.CLIENTS.FirstOrDefault((n) => name.Equals(n.clientName))?.clientId;
        }
    }
}
