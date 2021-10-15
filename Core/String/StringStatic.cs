using System;
using System.Text;

namespace MyIssue.Core.String
{
    public static class StringStatic
    {
        public static byte[] ByteMessage(string input)
        {
            try
            {
                return Encoding.UTF8.GetBytes(input);
            } catch (ArgumentNullException)
            {
                return new byte[0];
            }

        }
        public static string StringMessage(byte[] input, int length)
        {
            try
            {
                return Encoding.UTF8.GetString(input, 0, length).Replace("\u0000", "");
            } catch (ArgumentNullException)
            {
                return string.Empty;
            }

        }
        public static string[] CommandSplitter(string input, string splitString)
        {
            try
            {
                return input.Split(new string[] { splitString }, StringSplitOptions.None);
            } catch (NullReferenceException)
            {
                return new string[0];
            }

        }
        public static string[] SplitBrackets(string input, char firstbracket, char secondbracket)
        {
            return input.Split(firstbracket, secondbracket);
        }
        public static string CutString(string input)
        {
            try
            {
                return input.Length <= 15 ? input : input.Substring(0, 15);
            } catch (NullReferenceException)
            {
                return string.Empty;
            }

        }
        public static DateTime? CheckDate(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) return null;
            return Convert.ToDateTime(dateString);
        }
    }
}
