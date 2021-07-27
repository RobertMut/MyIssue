using MyIssue.Core.Entities.Database;
using System;

namespace MyIssue.Core.Interfaces
{
    public interface IUserRepository : IRepository<USER>
    {
        void AddUser(string[] input);
        Decimal? TypeLogin(string[] input);
    }
}