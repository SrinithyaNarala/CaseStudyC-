﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class AdminNotFoundException : Exception
    {
        public AdminNotFoundException() : base("Admin user not found.")
        {
        }
        public AdminNotFoundException(string message) : base(message) { }
    }
}
