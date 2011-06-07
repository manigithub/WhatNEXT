//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Ninject;

//namespace WhatNEXT
//{
//    public class WhatNEXT
//    {
        
//        private static IKernel kernel;

//        public static void Main()
//        {
//            ITaskList t = WhatNextFacade.GetInstance().CreateList();

//            ConsoleTaskEventNotifier.TaskEventNotifier(t);

//            t.AddTask(new TaskItem() { ID = 1 });

//            System.Threading.Thread.Sleep(10000);

//        }

//        private static void BootStrap()
//        {
//            kernel = new StandardKernel();

//            kernel.Bind<ITaskList>().To<TaskEventGenerator>();//TaskEventGenerator
//            //kernel.Bind<ITaskListWithEvents>().To<TaskEventGenerator>();
//            kernel.Bind<ITaskList>().To<SimpleTaskList>().WhenInjectedInto<TaskEventGenerator>();

//            //kernel.Bind<WhatNextFacade>().ToSelf();

//            tasklistFactory = kernel.Get<WhatNextFacade>();

//        }
//    }
//}
