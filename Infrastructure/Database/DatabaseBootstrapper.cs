using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Database.Models;
using System;
using System.Linq;

namespace MyIssue.Infrastructure.Database
{
    public class DatabaseBootstrapper : IDatabaseBootstrapper
    {
        private MyIssueContext _context;
        public DatabaseBootstrapper(MyIssueContext context)
        {
            _context = context;
        }
        public DatabaseBootstrapper(string sqlConnectionString)
        {
            _context = new MyIssueContext(sqlConnectionString);
        }
        public void Configure()
        {
            if (_context.Database.CreateIfNotExists())
            {
                _context.UserTypes.Add(new UserType()
                {
                    Name = "Locked"
                });
                _context.UserTypes.Add(new UserType()
                {
                    Name = "Normal"
                });
                _context.UserTypes.Add(new UserType()
                {
                    Name = "Admin"
                });
                _context.TaskTypes.Add(new TaskType()
                {
                    TypeName = "Low priority"
                });
                _context.TaskTypes.Add(new TaskType()
                {
                    TypeName = "Normal"
                });
                _context.TaskTypes.Add(new TaskType()
                {
                    TypeName = "Urgent"
                });
                _context.SaveChanges();
                var type = _context.UserTypes.FirstOrDefault(e => e.Name == "Admin").Id;
                _context.Users.Add(new User()
                {
                    UserLogin = "Admin",
                    Password = "1234",
                    UserType = type
                });
                _context.SaveChanges();
                Console.WriteLine("DB - {0} - Created database", DateTime.Now);
            }
        }
    }
}
