using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public class TaskScheduleEventArgs : EventArgs
    {
        private readonly TaskItem task;

        public TaskScheduleEventArgs(TaskItem task)
        {
            this.task = task;
        }

        public TaskItem Task
        {
            get { return task; }

        }
    }
}
