﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public class TaskAddEventArgs:EventArgs
    {
        private readonly TaskItem task; 
        public TaskAddEventArgs(TaskItem task)
        {
            this.task = task;
        }

        public TaskItem Task
        {
            get { return task; }
            
        }
    }
}
