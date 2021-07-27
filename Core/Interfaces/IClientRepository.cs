namespace MyIssue.Core.Interfaces
{
    public interface IClientRepository
    {
        decimal? GetClientByName(string name);
    }
}