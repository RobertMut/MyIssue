
namespace MyIssue.Core.Interfaces
{
    public interface IStringTools
    {
        byte[] ByteMessage(string input);
        string StringMessage(byte[] input, int length);
        int? NullableInt(string input);
        string[] CommandSplitter(string input, string splitString);
        string[] SplitBrackets(string input, char firstbracket, char secondbracket);
    }
}
