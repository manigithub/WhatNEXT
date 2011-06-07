using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace WhatNEXT
{
    public class TaskListFactory
    {
        private static TaskListFactory tasklistFactory = null; //new TaskListFactory();
        private static IKernel kernel;

        public TaskListFactory()
        {

        }
        private static void BootStrap()
        {
            kernel = new StandardKernel();

            kernel.Bind<ITaskList>().To<TaskEventGenerator>();//TaskEventGenerator
            //kernel.Bind<ITaskListWithEvents>().To<TaskEventGenerator>();
            kernel.Bind<ITaskList>().To<SimpleTaskList>().WhenInjectedInto<TaskEventGenerator>();

            kernel.Bind<ITaskParser>().To<TaskTextParser>();

            //kernel.Bind<TaskListFactory>().ToSelf();

            tasklistFactory = kernel.Get<TaskListFactory>();
        }

        public static TaskListFactory GetInstance()
        {

            if (tasklistFactory == null)
            {
                BootStrap();
            }

            return tasklistFactory;

        }
        public ITaskList CreateList()
        {
            return kernel.Get<ITaskList>();
        }
        public ITaskParser CreateTaskParser()
        {
            return kernel.Get<ITaskParser>();
        }
        public ITaskListWithEvents CreateTaskListWithEvents()
        {
            return kernel.Get<ITaskListWithEvents>();
        }

        //public static void Main()
        //{
        //    Console.WriteLine(TaskListFactory.GetInstance().CreateList());
        //}


    }
}
