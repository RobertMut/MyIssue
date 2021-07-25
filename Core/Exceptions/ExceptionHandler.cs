﻿using System;
using System.IO;

namespace MyIssue.Core.Exceptions
{
    public static class ExceptionHandler
    {
        public static void HandleMyException(Exception e)
        {
            File.CreateText("exception.txt").Dispose();
            string ex = string.Format("Exception occured - {0}\nMessage: \n{1}\nStack trace: {2}\n\n", DateTime.Now, e.Message, e.StackTrace);
            Console.WriteLine(ex);
            using (StreamWriter w = File.AppendText("exception.txt"))
            {
                w.WriteLine(ex);
                w.Close();
            }
        }
    }
}