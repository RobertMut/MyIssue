
namespace MyIssue.Core.Interfaces
{
    public interface IStringTools
    {
        int? NullableInt(string input);
        string[] SplitBrackets(string input, char firstbracket, char secondbracket);
    }
}
