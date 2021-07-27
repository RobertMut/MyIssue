using MyIssue.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyIssueDatabase _context;
        public ITaskRepository Task { get; private set; }
        public IUserRepository User { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public UnitOfWork(MyIssueDatabase context)
        {
            _context = context;
            Task = new TaskRepository(_context);
            User = new UserRepository(_context);
            Employee = new EmployeeRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
