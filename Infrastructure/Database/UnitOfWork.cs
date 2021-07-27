using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Database.Models;

namespace MyIssue.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyIssueDatabase _context;
        public ITaskRepository Task { get; private set; }
        public IUserRepository User { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IClientRepository Client { get; private set; }
        public UnitOfWork(MyIssueDatabase context)
        {
            _context = context;
            Task = new TaskRepository(_context);
            User = new UserRepository(_context);
            Employee = new EmployeeRepository(_context);
            Client = new ClientRepository(_context);
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
