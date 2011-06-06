using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WhatNEXT
{
    public class TaskItem
    {
        public Int64 ID { get; set; }
        public string Details { get; set; }
        public IPAddress IpAddress { get; set; }
    }
}
