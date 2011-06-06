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
   
    public class SimpleTaskListTests
    {
      

        [Test]
        public void AddSingleTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1});

            Assert.AreEqual(list.GetCount(), 1);
        }

        [Test]
        public void AddDualTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1 });
            list.AddTask(new TaskItem() { ID = 2 });

            Assert.AreEqual(list.GetCount(), 2);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void AddExistingTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1 });
            list.AddTask(new TaskItem() { ID = 1 });

            Assert.Throws<ApplicationException>(() => { throw new ApplicationException(); });

        }
   
        [Test]
        public void FindTaskByID()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();

            list.AddTask(new TaskItem() { ID = 1 });

            TaskItem t = list.FindTaskByID(1);

            Assert.IsTrue(t.ID == 1);
        }

        [Test]
        public void UpdateTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            TaskItem taskBeforeUpdate = list.FindTaskByID(1);
            list.UpdateTask(new TaskItem() { ID = 1, Details = "Updated Task Details" });
            TaskItem taskAfterUpdate = list.FindTaskByID(1);
            Assert.AreNotEqual(taskBeforeUpdate.Details, taskAfterUpdate.Details);
        }



        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateTask_To_TaskListWithZeroTasks_ThrowsTaskDoesNotExistException()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            list.UpdateTask(new TaskItem() { ID = 1000 });
            Assert.Throws<ApplicationException>(() => { throw new ApplicationException(); });

        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateTask_To_TaskListWithOneOrMoreTasks_TaskIDDoesNotExist_ThrowsTaskDoesNotExistException()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            list.UpdateTask(new TaskItem() { ID = 1001 });
            Assert.Throws<ApplicationException>(() => { throw new ApplicationException(); });
        }


        [Test]
        public void DeleteTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            list.DeleteTask(new TaskItem(){ID = 1});
            Assert.IsTrue(list.GetCount() == 0);
        }

        [Test]
        public void DeleteMultipleTasks()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            list.AddTask(new TaskItem() { ID = 2 });
            list.AddTask(new TaskItem() { ID = 3 });
            list.DeleteTask(new TaskItem() { ID = 2 });
            list.DeleteTask(new TaskItem() { ID = 3 });
            Assert.IsTrue(list.GetCount() == 1);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void DeleteInvalidTask()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.DeleteTask(new TaskItem() { ID = 10 });
            Assert.Throws<ApplicationException>(() => { throw new ApplicationException(); });
            
        }


        [Test]
        public void GetTasksList()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1 });
            list.AddTask(new TaskItem() { ID = 2 });
            list.AddTask(new TaskItem() { ID = 3 });

            Assert.AreEqual(list.GetAll(), 3);
        }

        [Test]
        public void ExportTasks()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1, Details = "Task one details"});
            list.AddTask(new TaskItem() { ID = 2, Details = "Task one details" });
            list.AddTask(new TaskItem() { ID = 3, Details = "Task one details" });//TODO: Check with IP address

            
            Assert.IsTrue(list.ExportTask(list.GetAll(), Enumerations.ContentType.XML).GetType()==typeof(byte[]));
            Assert.IsTrue(list.ExportTask(list.GetAll(), Enumerations.ContentType.XML).Length > 0);
        }

        [Test]
        public void ExportTasksToXML()
        {
            ITaskList list = TaskListFactory.GetInstance().CreateList();
            list.AddTask(new TaskItem() { ID = 1, Details = "Task one details" });
            list.AddTask(new TaskItem() { ID = 2, Details = "Task one details" });
            list.AddTask(new TaskItem() { ID = 3, Details = "Task one details" });//TODO: Check with IP address


            byte[] buffer = list.ExportTask(list.GetAll(), Enumerations.ContentType.XML);
            using (Stream s = new FileStream ("test.xml", FileMode.Create))
            {
                s.Write(buffer, 0, buffer.Length);
              
            }
        }

        //public static void Main(string[] args)
        //{
        //    NUnit.ConsoleRunner.Runner.Main();
            
        //}
    }

    

}
