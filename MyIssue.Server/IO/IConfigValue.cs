﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.IO
{
    public interface IConfigValue
    {
        string GetValue(string node);
    }
}
