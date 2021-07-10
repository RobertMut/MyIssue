﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Core.Interfaces
{
    public interface IDecryptedValue
    {
        string GetValue(string value, string key = null);
    }
}
