using MyIssue.Core.Interfaces;
using System;
using System.Text;

namespace MyIssue.Core.String
{
    public class StringTools : IStringTools
    {


        public int? NullableInt(string input)
        {
            if (int.TryParse(input, out int i)) return i;
            return null;
        }
        public string[] SplitBrackets(string input, char firstbracket, char secondbracket)
        {
            return input.Split(firstbracket, secondbracket);
        }
    }
}
