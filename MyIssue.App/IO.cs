using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace MyIssue.App
{
    public class IO : IDisposable
    {
        private static XmlWriterSettings writerSettings = new XmlWriterSettings();
        public static List<string> encryptedData = new List<string>(); //to array
        public static List<string> decryptedData = new List<string>(); //to array
        public static bool configurationFlag;
        private static bool registryClosed = false;
        private bool _disposed = true;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        public IO()
        {
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Auto;
            writerSettings.Indent = true;
            writerSettings.NewLineChars = "\r\n";
            writerSettings.NewLineOnAttributes = true;
            writerSettings.Encoding = Encoding.UTF8;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public List<string> ConfigurationReader()
        {
            List<string> worklist = new List<string>();
            string regHash = GetRegistryCrc();
            string fileHash = FileInfomation(MainWindow.confFile);
            if (!fileHash.Equals(regHash))
            {
                configurationFlag = true;
                Debug.WriteLine("Hash mismatch! Deleting old configuration file..");
                if (File.Exists(MainWindow.confFile)) File.Delete(MainWindow.confFile);
                return worklist;
            }
            else
            {
                configurationFlag = false;
                {
                    try
                    {
                        using (XmlReader xRead = XmlReader.Create(MainWindow.confFile))
                        {


                            while (xRead.Read())
                            {
                                switch (xRead.NodeType)
                                {
                                    //wyswietla 0;
                                    //case XmlNodeType.Element:
                                        //Debug.WriteLine(xRead.Name);
                                        //break;
                                    case XmlNodeType.Text:
                                        Debug.WriteLine(xRead.Value);
                                        worklist.Add(xRead.Value);
                                        break;
                                }

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Reading configuration exception -> {0}", e.Message);
                    }

                }
                return worklist;
            }
        }
        public string[] FormReader()
        {
            List<string> values = new List<string>();
            if (File.Exists(MainWindow.userFile))
            {
                try
                {
                    using (XmlReader xRead = XmlReader.Create(MainWindow.userFile))
                    {
                        xRead.MoveToContent();
                        while (xRead.Read())
                        {
                            switch (xRead.NodeType)
                            {
                                case XmlNodeType.Element:
                                    break;
                                case XmlNodeType.Text:
                                    values.Add(xRead.Value);
                                    break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Reading user's XML exception -> {0}", e.Message);
                }
            }
            return values.ToArray();
        }
        public void ConfigurationCreator(string aPass, string[] settings, bool[] encryption)
        {
            string[] dataTitle = { "server", "port", "login", "userP", "mail", "recipient", "aPass", "buisnessName", "template", "image" };
            string[] additionalData = { "connMethod","sslTsl", "bCompanyName"};
            string bName = settings[settings.Length - 3];
            string[] encryptedData = new string[6];
            string[] encSettingString = new string[3];
            for (int i = 0; i < encryption.Length; i++) {
                encSettingString[i] = encryption[i].ToString();
            }
            if (!Directory.Exists(MainWindow.path)) Directory.CreateDirectory(MainWindow.path);
            encryptedData[0] = Crypto.AesEncrypt(aPass, bName); 
            for (int i = 1; i < settings.Length-3; i++)
            {

                encryptedData[i] = Crypto.AesEncrypt(settings[i], aPass); 
            }
            try
            {
                if (File.Exists(MainWindow.confFile)) File.Delete(MainWindow.confFile);
                using (XmlWriter writer = XmlWriter.Create(MainWindow.confFile, writerSettings))
                {
                    writer.WriteStartDocument(true);
                    writer.WriteStartElement("configuration"); //configuration section
                    writer.WriteElementString(dataTitle[6], encryptedData[0]); //writes apass
                    writer.WriteElementString(dataTitle[7], bName); //writes businessname
                    WriteMultipleString(writer, dataTitle, encryptedData.Skip(1).ToArray(), dataTitle.Length - 4, encryptedData.Length);
                    WriteMultipleString(writer, additionalData, encSettingString, additionalData.Length, encryption.Length);
                    WriteMultipleString(writer, dataTitle.Skip(dataTitle.Length - 2).ToArray(), settings.Skip(settings.Length - 2).ToArray(), dataTitle.Length - 2, settings.Length - 2);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine("Writing XML exception -> {0}", e.Message);


            }
            SetRegistryCrc(FileInfomation(MainWindow.confFile));
            IO.encryptedData = ConfigurationReader();
            configurationFlag = false;
        }
        private static void WriteMultipleString(XmlWriter writer, string[] titleTable, string[] stringTable, int titleLenght, int stringLenght)
        {
            for (int i = 0; i < titleLenght; i++)
            {
                writer.WriteElementString(titleTable[i], stringTable[i]);
            }
            
        }
        public void FormWriter(bool saveData, string path, string userFile)
        {

            switch (saveData)
            {
                case true:

                    string[] dataTitle = { "name", "surname", "phoneNumber", "email" };
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    try
                    {
                        if (File.Exists(userFile)) File.Delete(userFile);
                        using (XmlWriter writer = XmlWriter.Create(userFile, writerSettings))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("userData");
                            for (int i = 0; i < dataTitle.Length - 1; i++)
                            {
                                switch (i) { case 2: i++; break; }
                                writer.WriteElementString(dataTitle[i], MainWindow.input[i]);
                            }
                            writer.WriteElementString("saveBox", saveData.ToString());
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Writing user's XML exception -> {0}", e.Message);
                    }

                    break;
            }
        }
        private static string GetRegistryCrc()
        {
            string hash = "ERROR";
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SupportApp", true))
            {
               
                if (regKey != null) hash = (String)regKey.GetValue("Hash");
                regKey.Close();
                registryClosed = true;
            }
            return hash;
        }
        private static string FileInfomation(string confFile)
        {
            if (File.Exists(confFile))
            {
                Console.WriteLine("MD5 "+Crypto.CalculateMD5(confFile));
                return Crypto.CalculateMD5(confFile);
            }
            else
            {
                Debug.WriteLine("File hash does not exist!");
                return "ERR";
            }


        }
        private static void SetRegistryCrc(string hash)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SupportApp", true))
                {
                    if (registryClosed.Equals(true)) regKey.OpenSubKey(@"SOFTWARE\SupportApp", true);
                    try
                    {
                        if (!(regKey is null))
                        {
                            regKey.SetValue("Hash", hash, RegistryValueKind.String);
                        }

                        
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Writing registry exception -> {0}", e.Message);
                    }
                }
            } catch (Exception e)
            {
                Debug.WriteLine("Opening registry exception -> {0}", e.Message);
            }
            
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;

            }
            if (disposing)
            {
                _safeHandle?.Dispose();
            }
            _disposed = true;
            Dispose(disposing);
        }
    }

}
/* What does config contain:
 * 0 application password
 * 1 company name
 * 2 server
 * 3 port
 * 4 login
 * 5 password
 * 6 mail
 * 7 recipient
 * 8 conn method
 * 9 ssltsl bool
 * 10 is companyname set bool
 * 11 email template
 * 12 image
 * 
 */
