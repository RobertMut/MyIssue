using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyIssue.DesktopApp.Model
{
    public static class Crypto
    {
        private const string builtInKey = "~<6+EqhtP9,aw6(A\\p";
        public static string AesEncrypt(string text, string key = builtInKey)
        {
            byte[] iv = BitsGenerator();
            byte[] encrypted;
            using (RijndaelManaged rij = new RijndaelManaged())
            {
                rij.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
                rij.IV = iv;
                using (ICryptoTransform encryptor = rij.CreateEncryptor(rij.Key, rij.IV))
                using (MemoryStream memStr = new MemoryStream())
                using (CryptoStream cryStr = new CryptoStream(memStr, encryptor, CryptoStreamMode.Write))
                {

                    using (StreamWriter writer = new StreamWriter(cryStr))
                    {
                        writer.Write(text);
                    }
                    encrypted = memStr.ToArray();
                    encrypted = encrypted.Concat(iv).ToArray();
                    return Convert.ToBase64String(encrypted);
                }
            }
        }
        public static string AesDecrypt(string input, string key = builtInKey)
        {
            byte[] byteInput = Convert.FromBase64String(input);
            byte[] iv = new byte[16];
            byte[] data = new byte[byteInput.Length - 16];
            Array.Copy(byteInput, byteInput.Length - 16, iv, 0, iv.Length);
            Array.Copy(byteInput, 0, data, 0, byteInput.Length - 16);
            string plaintext;
            using (RijndaelManaged rij = new RijndaelManaged())
            {
                rij.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
                rij.IV = iv;
                using (ICryptoTransform decryptor = rij.CreateDecryptor(rij.Key, rij.IV))
                using (MemoryStream memStr = new MemoryStream(data))
                using (CryptoStream cryStr = new CryptoStream(memStr, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cryStr))
                    {
                        plaintext = sr.ReadToEnd();
                    }
                    return plaintext;
                }
            }
        }
        private static byte[] BitsGenerator()
        {
            byte[] bytes = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(bytes);
            return bytes;
        }
    }
}


