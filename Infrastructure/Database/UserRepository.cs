using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Infrastructure.Database.Models
{
    public class UserRepository : Repository<USER>, IUserRepository
    {
        public UserRepository(MyIssueDatabase context) : base(context)
        {

        }
        public Decimal? TypeLogin(string[] input)
        {
            var q = _context.USERS.First((l) => input[0].Equals(l.userLogin) && input[1].Equals(l.password));
            return q.type;
        }
        public void AddUser(string[] input)
        {
            _context.USERS.Add(new USER
            {
                userLogin = input[0],
                password = input[1],
                type = Decimal.Parse(input[2])
            });
        }
        public void DeleteUser(string[] input)
        {
            throw new NotImplementedException(); //TODO
        }
        public void ChangeUserType(string[] input)
        {
            throw new NotImplementedException();
        }
    }
}
