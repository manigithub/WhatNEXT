using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
    public class TaskReminder
    {

        public TaskReminder()
        {
            ITaskList list = WhatNextFacade.GetInstance().CreateList();
            TaskScheduler taskScheduler = TaskScheduler.GetTaskScheduler();
            ((ITaskListWithEvents)list).Add += new AddTaskEventHandler(taskScheduler.TaskScheduler_Add);
            ScheduledTasksLogger tasksLogger = new ScheduledTasksLogger();
            taskScheduler.Schedule += new TaskSchedulerEventHandler(tasksLogger.taskScheduler_Schedule);
        }

        public void RemindTask(string taskDetails)
        {
            TaskItem taskItem = WhatNextFacade.GetInstance().CreateTaskParser().Parse(taskDetails);
            WhatNextFacade.GetInstance().CreateList().AddTask(taskItem);
            Console.WriteLine("Main Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        public static TaskReminder GetInstance()
        {
            return WhatNextFacade.GetInstance().TaskReminder();
        }
    }
}
