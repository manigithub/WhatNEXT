using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public class TaskAddEventArgs:EventArgs
    {
        private Int64 taskID;

        public long TaskID
        {
            get { return taskID; }
            private set { taskID = value; }
        }

        public TaskAddEventArgs(Int64 taskID)
        {
            TaskID = taskID;
        }
    }
}
