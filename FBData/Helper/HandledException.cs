﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBData.Helper
{
    public class HandledException : Exception
    {
        public HandledException(string? message) : base(message) { }
    }
}
