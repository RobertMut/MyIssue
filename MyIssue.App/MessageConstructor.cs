using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Management;

namespace MyIssue.App
{
    public class MessageConstructor : IDisposable
    {
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private string[] message = new string[] {
                "EHLO " + IO.decryptedData[1] + "\r\n",
                "AUTH LOGIN\r\n",
                Crypto.B64E(IO.decryptedData[3]) + "\r\n",
                Crypto.B64E(IO.decryptedData[4]) + "\r\n",
                "MAIL FROM:<" + IO.decryptedData[5] + ">\r\n",
                "RCPT TO:<" + IO.decryptedData[6] + ">\r\n",
                "DATA\r\n", "", "QUIT\r\n",

            };
        public string[] HTMLMessageBuilder(List<string> worklist)
        {
            string template;
            string templatePath = Environment.ExpandEnvironmentVariables(@"%APPDATA%\SimpleIssueReporting\template.html");
            if (!File.Exists(templatePath))
                template = Cli.HtmlTemplate(IO.encryptedData[11]);
            else template = File.ReadAllText(templatePath);

            
            string main =  
                "From: SimpleIssueReporting\r\n" +
                "Subject: " + "[" + worklist[2] + "] " + worklist[5].Substring(0, worklist[5].Length / 2) + " \r\n" +
                "To: " + IO.decryptedData[6] + "\r\n" +
                "Mime-Version: 1.0\r\n" +
                "Content-Type: text/html; charset=\"utf-8\";\r\n" +
                "Content-Transfer-encoding: 8bit;\r\n";

            worklist.AddRange(SpecsString());
            main += StrBuilder(worklist, template).ToString();
            main += "\r\n.\r\n";
                message[7] = main;


            return message;
        }
        public string[] MessageBuilder(List<string> worklist)
        {
            string main = 
                "From: SupportApp\r\n" +
                "Subject: " + "[" + worklist[2] + "] " + worklist[5].Substring(0, worklist[5].Length / 2) + " \r\n" +
                "To: " + IO.decryptedData[6] + "\r\n";
            worklist.AddRange(SpecsString());
            main += StrBuilder(worklist).ToString();
            main += "\r\n.\r\n";
            message[7] = main;
            return message;
        }
        private string[] SpecsString()
        {
            DriveInfo dInfo = new DriveInfo(@"C:");
            List<string> worklist = new List<string>();
            int totalMem = Int32.Parse(InformationSearcher("SELECT * FROM Win32_OperatingSystem", "TotalVisibleMemorySize")) / 1024;
            int freeMem = Int32.Parse(InformationSearcher("SELECT * FROM Win32_OperatingSystem", "FreePhysicalMemory")) / 1024;
            worklist.Add(InformationSearcher("SELECT * FROM Win32_OperatingSystem", "OsArchitecture")+"\r\n");
            worklist.Add(InformationSearcher("SELECT * FROM Win32_Processor", "Name") + "\r\n");
            worklist.Add(totalMem + "MB");
            worklist.Add(freeMem + "MB");
            worklist.Add((dInfo.TotalSize/1024/1024/1024).ToString() +"GB");
            worklist.Add((dInfo.AvailableFreeSpace / 1024 / 1024 /1024).ToString() +"GB");
            return worklist.ToArray();
        }
        private static StringBuilder StrBuilder(List<string> newText, string file = "0")
        {
            StringBuilder sb = new StringBuilder(file);
            if (!file.Equals("0"))
            {
                
                for (int i = 0; i < newText.Count; i++)
                {
                    Debug.WriteLine("input[" + i + "] " + newText[i]);
                    sb.Replace("input[" + i + "]", newText[i]);
                }
                return sb;
            } else
            {
                string[] titles = { "Name: ", "Surname: ", "Company: ", "Phone Number: ", "Email: ", "Description: ", 
                    "System: ", "Processor: ", "Available Memory: ", "Free Memory: ", "Partition Space: ", "Free Partiton Space: " };
                for (int i = 0; i < titles.Length; i++)
                {
                    sb.AppendLine(titles[i] + newText[i]);
                }
                return sb;
            }

        }
        public static string InformationSearcher(string query, string value)
        {
            string response = null;
            ManagementScope scope = new ManagementScope(@"\\localhost\root\CIMV2");
            scope.Connect();
            SelectQuery sQuery = new SelectQuery(query);
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher(scope, sQuery))
            {
                foreach (ManagementObject mObject in objectSearcher.Get())
                {
                    response = mObject[value].ToString();
                    //Debug.WriteLine(response);
                }
            }

            if (response.Equals(string.Empty))
            {
                return "#######";
            }
            else
            {
                return response;
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
