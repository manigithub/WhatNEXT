using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using NUnit;
using NUnit.ConsoleRunner;
using System.IO;
using Ninject;
using WhatNEXT;

namespace WhatNEXT.Tests
{
    [TestFixture]
    public class TaskSchedulerTests
    {

        [Test]
        public void ScheduleOneTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1 });

            ITaskScheduler scheduler = TaskListFactory.GetInstance().CreateScheduleList();

            scheduler.Schedule += delegate(object sender, ScheduledTaskItemEventArgs e)
            {
               
            };


        }

    }
}
