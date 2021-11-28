using MyIssue.DesktopApp.Misc.Utility;
using NUnit.Framework;

namespace MyIssue.UnitTests
{
    class CryptoTests
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void EncryptTest()
        {
            Assert.IsTrue(44 == Crypto.AesEncrypt("2136").Length);
            Assert.DoesNotThrow(() => Crypto.AesEncrypt(null));
            Assert.DoesNotThrow(() => Crypto.AesEncrypt(string.Empty));
        }
        [Test]
        public void DecryptTest()
        {
            string teststr = "superstring";
            string emptystr = string.Empty;
            Assert.DoesNotThrow(() =>
            {
                var test1 = EncryptDecrypt(teststr, "superkey");
                var test2 = EncryptDecrypt(string.Empty, "superkey");
                var test3 = EncryptDecrypt(teststr);
                var test4 = EncryptDecrypt(string.Empty);
                Assert.AreEqual(teststr, test1);
                Assert.AreEqual(string.Empty, test2);
                Assert.AreEqual(teststr, test3);
                Assert.AreEqual(string.Empty, test4);
            });

        }
        private string EncryptDecrypt(string testString, string key)
        {
            string enc = Crypto.AesEncrypt(testString, key);
            string dec = Crypto.AesDecrypt(enc, key);
            return dec;
        }
        private string EncryptDecrypt(string testString)
        {
            string enc = Crypto.AesEncrypt(testString);
            string dec = Crypto.AesDecrypt(enc);
            return dec;
        }
    }
}
