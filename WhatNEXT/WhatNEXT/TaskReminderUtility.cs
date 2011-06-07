using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
    public static class TaskReminderUtility
    {
        public static void RemindTask(string taskDetails)
        {
            ITaskParser taskParser = TaskListFactory.GetInstance().CreateTaskParser();
            TaskItem taskItem = taskParser.Parse("taskDetails");

            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(taskItem);

            TaskScheduler taskScheduler = new TaskScheduler();
            CommandInterpreter commandInterpreter = new CommandInterpreter(taskScheduler);
            
            Console.WriteLine("Main Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.CurrentThread.Join(5000);


            
        }
    }
}
