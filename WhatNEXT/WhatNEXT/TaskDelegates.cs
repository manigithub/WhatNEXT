using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public delegate void AddTaskEventHandler(object sender, TaskAddEventArgs e);
    public delegate void TaskSchedulerEventHandler(object sender, TaskScheduleEventArgs e);
    public delegate void RemindMe(object sender, TaskScheduleEventArgs e);
}
