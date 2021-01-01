﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Utils.Helpers.LoginConfiguration
{
    public class Token
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public double Seconds { get; set; }
    }
}
