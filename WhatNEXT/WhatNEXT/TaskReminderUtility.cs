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
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            TaskScheduler taskScheduler = TaskScheduler.GetTaskScheduler();
            ((ITaskListWithEvents)list).Add += new AddTaskEventHandler(taskScheduler.TaskScheduler_Add);
            CommandInterpreter commandInterpreter = new CommandInterpreter();
            ScheduledTasksLogger tasksLogger = new ScheduledTasksLogger();
            taskScheduler.Schedule += new TaskSchedulerEventHandler(tasksLogger.taskScheduler_Schedule);
            taskScheduler.Schedule += new TaskSchedulerEventHandler(commandInterpreter.taskScheduler_Schedule);
            ITaskParser taskParser = TaskListFactory.GetInstance().CreateTaskParser();
            TaskItem taskItem = taskParser.Parse(taskDetails);

            list.AddTask(taskItem);
            Console.WriteLine("Main Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
