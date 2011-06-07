using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public interface ITaskParser
    {
        TaskItem Parse(string taskDetails);
    }
}
