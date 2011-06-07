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
    public class TaskEventGeneratorTests
    {
        [Test]
        public void TestAddTaskEvent()
        {
            ITaskList list = WhatNextFacade.GetInstance().CreateList();

            ((ITaskListWithEvents)list).Add +=
                delegate(object sender, TaskAddEventArgs e)
                    {
                        Console.WriteLine("event fired intest cse");
                        Assert.AreEqual(e.Task.ID, 1);
                            
                };
            list.AddTask(new TaskItem() { ID = 1 });
        }
    }
}
