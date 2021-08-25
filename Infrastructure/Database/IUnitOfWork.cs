namespace MyIssue.Infrastructure.Database
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}