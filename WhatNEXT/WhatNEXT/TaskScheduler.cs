using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;

namespace WhatNEXT
{
    public class TaskScheduler
    {
        public event TaskSchedulerEventHandler Schedule;
        private static TaskScheduler taskScheduler;
        private ConcurrentQueue<Timer> timerQueue = new ConcurrentQueue<Timer>();
        private readonly  object locker = new object();
        //private FileStream fs = File.OpenWrite(@"C:\Users\manikandan\Desktop\TaskSchduler.txt");
        private TextWriter writer = File.CreateText(@"C:\Users\manikandan\Desktop\TaskSchduler.txt");

        public static TaskScheduler GetTaskScheduler()
        {
            if (taskScheduler == null)
            {
                taskScheduler = new TaskScheduler();
            }
            return taskScheduler;
        }

        public static void Main()
        {
            //TaskScheduler taskScheduler = new TaskScheduler();
            //CommandInterpreter commandInterpreter = new CommandInterpreter(taskScheduler);
            //taskScheduler.ScheduleTask();
            //Console.WriteLine("Main Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            //Thread.CurrentThread.Join(5000);
        }
        // Invoke the Changed event; called whenever list changes
        protected virtual void OnSchedule(object e)
        {
            if (Schedule != null)
            {
                var temp = Schedule;
                writer.WriteLine("Invoking Task ID:" + ((TaskScheduleEventArgs)e).Task.ID + " Task Details: " + ((TaskScheduleEventArgs)e).Task.Details);
                writer.Flush();
                temp(this, (TaskScheduleEventArgs)e);
            }
        }
        private TaskScheduler()
        {
        //    ITaskList list = WhatNextFacade.GetInstance().CreateList();

        //    ((ITaskListWithEvents)list).Add += new AddTaskEventHandler(TaskScheduler_Add);

        }
        public void TaskScheduler_Schedule(object sender, TaskAddEventArgs e)
        {
             lock (locker)
             {
                 writer.WriteLine("Task ID:" + e.Task.ID + " Task Details: " + e.Task.Details);
                 writer.Flush();
                 ScheduleTask(e.Task);
             }
        }
        private void ScheduleTask(TaskItem taskItem)
        {
           
                timerQueue.Enqueue( new Timer(new TimerCallback(OnSchedule),
                                        new TaskScheduleEventArgs(taskItem), taskItem.TimeReminder!=0?taskItem.TimeReminder:1000000, Timeout.Infinite));
           
            
        }
        public void ScheduleDummyTask()
        {
            ITaskList list = WhatNextFacade.GetInstance().CreateTaskList();

            ((ITaskListWithEvents)list).Add +=
                                            delegate(object sender, TaskAddEventArgs e)
                                            {
                                                Timer t = new Timer(new TimerCallback(OnSchedule));

                                                Timer taskTimer = new Timer(new TimerCallback(OnSchedule),
                                                                            new TaskScheduleEventArgs(e.Task), 2000, Timeout.Infinite);
                                            };

            list.AddTask(new TaskItem() { ID = 1, Details = "schedule tasks", IpAddress = "1019292", TimeReminder = 10 });
        }
    }
}
