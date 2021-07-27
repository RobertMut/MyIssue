using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using System;
using System.Linq;

namespace MyIssue.Infrastructure.Database.Models
{
    public class UserRepository : Repository<USER>, IUserRepository
    {
        public UserRepository(MyIssueDatabase context) : base(context)
        {

        }
        public Decimal? TypeLogin(string[] input)
        {
            string login = input[0];
            string pass = input[1];
            return _context.USERS.Where(inp => inp.userLogin == login && inp.password == pass).Select(s => s.type).FirstOrDefault();
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
