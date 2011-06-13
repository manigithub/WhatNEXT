using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
   

    public class TaskReminder : ITaskReminder
    {

        public TaskReminder()
        {
            ITaskList list = WhatNextFacade.GetInstance().CreateTaskList();
            ((ITaskListWithEvents)list).Add += new AddTaskEventHandler(TaskScheduler.GetTaskScheduler().TaskScheduler_Schedule);
            ScheduledTasksLogger tasksLogger = new ScheduledTasksLogger();
            CallMeBack(tasksLogger.taskScheduler_Schedule);
        }

        public void CallMeBack(RemindMe CallBackMethod)
        {
            TaskScheduler.GetTaskScheduler().Schedule += new TaskSchedulerEventHandler(CallBackMethod);
        }

        public void RemindTask(string taskDetails)
        {
            TaskItem taskItem = WhatNextFacade.GetInstance().CreateTaskParser().Parse(taskDetails);
            RemindTask(taskItem);
        }

        public void RemindTask(TaskItem taskItem)
        {
            WhatNextFacade.GetInstance().CreateTaskList().AddTask(taskItem);
            //Console.WriteLine("Main Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        public static ITaskReminder GetInstance()
        {
            return WhatNextFacade.GetInstance().TaskReminder();
        }
    }
}
