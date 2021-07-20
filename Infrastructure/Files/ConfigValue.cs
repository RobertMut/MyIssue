﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace MyIssue.Infrastructure.Files
{
    public static class ConfigValue
    {
        public static string GetValue(string node, XDocument d)
        {
            try
            {
                return d?.Descendants(node).FirstOrDefault()?.Value;
            } 
            catch (NullReferenceException)
            {
                return null;
            }
                
        }
    }
}
