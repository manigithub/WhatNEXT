using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace WhatNEXT
{
    public class WhatNextFacade
    {
        private static WhatNextFacade facade = new WhatNextFacade();
        private readonly IKernel kernel;

        private WhatNextFacade()
        {
            kernel = new StandardKernel();
            kernel.Bind<TaskReminder>().ToSelf().InSingletonScope();//TaskEventGenerator
            kernel.Bind<ITaskList>().To<TaskEventGenerator>().InSingletonScope();//TaskEventGenerator
            kernel.Bind<ITaskList>().To<SimpleTaskList>().WhenInjectedInto<TaskEventGenerator>();
            kernel.Bind<ITaskParser>().To<TaskTextParser>();
        }

        public static WhatNextFacade GetInstance()
        {
            return facade;

        }
        public ITaskList CreateTaskList()
        {
            return kernel.Get<ITaskList>();
        }
        public ITaskParser CreateTaskParser()
        {
            return kernel.Get<ITaskParser>();
        }
        public TaskReminder TaskReminder()
        {
            return kernel.Get<TaskReminder>();
        }
        public ITaskListWithEvents CreateTaskListWithEvents()
        {
            return kernel.Get<ITaskListWithEvents>();
        }

        //public static void Main()
        //{
        //    Console.WriteLine(WhatNextFacade.GetInstance().CreateList().GetHashCode());
        //    Console.WriteLine(WhatNextFacade.GetInstance().CreateList().GetHashCode());
        //}


    }
}
