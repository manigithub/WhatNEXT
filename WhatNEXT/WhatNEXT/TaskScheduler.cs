using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
    public class TaskScheduler
    {
        public event TaskSchedulerEventHandler Schedule;
        private static TaskScheduler taskScheduler;

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
                Schedule(this, (TaskScheduleEventArgs)e);
            }
        }
        private TaskScheduler()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            ((ITaskListWithEvents)list).Add += new AddTaskEventHandler(TaskScheduler_Add);

        }
        void TaskScheduler_Add(object sender, TaskAddEventArgs e)
        {
             ScheduleTask(taskItem)
        }
        public void ScheduleTask(TaskItem taskItem)
        {
            Timer taskTimer = new Timer(new TimerCallback(OnSchedule),
                                        new TaskScheduleEventArgs(taskItem), taskItem.TimeReminder, Timeout.Infinite);
        }
        public void ScheduleDummyTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

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
