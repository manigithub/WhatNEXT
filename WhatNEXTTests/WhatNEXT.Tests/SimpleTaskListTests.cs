using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ninject;
using WhatNEXT;

namespace WhatNEXT.Tests
{
    
    [TestFixture]
   
    public class SimpleTaskListTests
    {
      

        [Test]
        public void AddSingleTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1});

            Assert.AreEqual(simpleTaskList.GetCount(), 1);
        }

        [Test]
        public void AddDualTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1 });
            list.AddTask(new TaskItem() { ID = 2 });

            Assert.AreEqual(simpleTaskList.GetCount(), 2);
        }
   
        [Test]
        public void FindTaskByID()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1 });

            TaskItem t = simpleTaskList.FindTaskByID(1);

            Assert.IsTrue(t.ID == 1);
        }

        [Test]
        public void UpdateTask()
        {

            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            TaskItem taskBeforeUpdate = simpleTaskList.FindTaskByID(1);
            simpleTaskList.UpdateTask(new TaskItem() { ID = 1, Details = "Updated Task Details" });
            TaskItem taskAfterUpdate = simpleTaskList.FindTaskByID(1);
            Assert.AreNotEqual(taskBeforeUpdate.Details, taskAfterUpdate.Details);
        }



        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateTask_To_TaskListWithZeroTasks_ThrowsTaskDoesNotExistException()
        {
            list.AddTask(new TaskItem() { ID = 1 });

            list.UpdateTask(new TaskItem() { ID = 1000 });

            Assert.Throws<ApplicationException>(() => { throw new ApplicationException(); });

        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateTask_To_TaskListWithOneOrMoreTasks_TaskIDDoesNotExist_ThrowsTaskDoesNotExistException()
        {

            var simpleTaskList = kernel.Get<ITaskList>();

            simpleTaskList.UpdateTask(new TaskItem() { ID = 1001 });

            Assert.Throws<ApplicationException>(() => { throw new ApplicationException(); });
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            kernel.Dispose();
        }
    }
}
