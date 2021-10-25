using MyIssue.Core.String;
using NUnit.Framework;

namespace MyIssue.UnitTests
{
    public class StringStaticTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NullTests()
        {
            byte[] message = null;
            string nullstr = null;
            Assert.DoesNotThrow(() => StringStatic.ByteMessage(nullstr));
            Assert.DoesNotThrow(() => StringStatic.StringMessage(message, 0));
            Assert.DoesNotThrow(() => StringStatic.CommandSplitter(nullstr, "\r\n"));
            Assert.DoesNotThrow(() => StringStatic.CutString(nullstr));
        }
        [Test]
        public void EmptyTests()
        {
            string emptystr = string.Empty;
            Assert.DoesNotThrow(() => StringStatic.ByteMessage(emptystr));
            Assert.DoesNotThrow(() => StringStatic.CommandSplitter(emptystr, "\r\n"));
            Assert.DoesNotThrow(() => StringStatic.CutString(emptystr));
        }
    }
}