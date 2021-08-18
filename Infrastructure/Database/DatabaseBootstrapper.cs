using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Database.Models;

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
            if (!_context.Database.Exists())
            {
                _context.Database.Create();
            }
            if (_context.Database.CompatibleWithModel(false))
            {
                _context.Database.Delete();
                _context.Database.Create();
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
            }
        }
    }
}
