namespace MyIssue.Main.API.Infrastructure
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}