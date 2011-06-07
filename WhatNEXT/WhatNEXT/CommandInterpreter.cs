using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
    public class CommandInterpreter
    {   
        public CommandInterpreter()
        {
        }

        public void taskScheduler_Schedule(object sender, TaskScheduleEventArgs e)
        {
            Console.WriteLine("command interpreter Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Command Interpreter: Task ID: {0} TaskTime: {1}", e.Task.ID, e.Task.TimeReminder);
        }
    }
}
