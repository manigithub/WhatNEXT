using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
    public class ScheduledTasksLogger
    {
        private TaskScheduler taskScheduler;
        public ScheduledTasksLogger(TaskScheduler taskScheduler)
        {
            this.taskScheduler = taskScheduler;
            this.taskScheduler.Schedule += new TaskSchedulerEventHandler(taskScheduler_Schedule);
        }

        void taskScheduler_Schedule(object sender, TaskScheduleEventArgs e)
        {
            Console.WriteLine("command interpreter Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Command Interpreter: Task ID: {0} TaskTime: {1}", e.Task.ID, e.Task.TimeReminder);
            WriteTaskToFile(e.Task);
        }

        private void WriteTaskToFile(TaskItem taskItem)
        {
            string mydocpath = @"C:\Users\manikandan\Desktop";
           //Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            var sb = new StringBuilder();
            sb.AppendLine("= = = = = =");
            sb.AppendLine(System.DateTime.Now.ToShortTimeString());
            sb.Append("Task ID:" + taskItem.ID);
            sb.Append("Task Details:" + taskItem.Details);
            sb.Append("Task Schduled:" + taskItem.TimeReminder);
            sb.AppendLine();
            sb.AppendLine("= = = = = =");

            using (var outfile =
                new StreamWriter(mydocpath + @"\TaskList.txt", true))
            {
                outfile.Write(sb.ToString());
            }
        }

    }
}
