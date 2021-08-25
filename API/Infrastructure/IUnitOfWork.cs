namespace MyIssue.API.Infrastructure
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}