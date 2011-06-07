using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public static class ConsoleTaskEventNotifier
    {
        

        public static void TaskEventNotifier(ITaskList taskList)
        {
            if(taskList != null)
            {
                ((ITaskListWithEvents)taskList).Add += new AddTaskEventHandler(ConsoleTaskEventNotifier_Add);
            }
        }
        static void ConsoleTaskEventNotifier_Add(object sender, TaskAddEventArgs e)
        {
            Console.WriteLine("Task id added: {0}", e.Task.ID);
        }

       
    }
}