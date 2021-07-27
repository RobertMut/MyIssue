using System;

namespace MyIssue.Infrastructure.Database.Models
{
    public interface IUserRepository : IRepository<USER>
    {
        void AddUser(string[] input);
        Decimal? TypeLogin(string[] input);
    }
}