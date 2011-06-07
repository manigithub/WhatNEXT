using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public class TaskEventGenerator : ITaskListWithEvents
    {
        private ITaskList taskList;//why private - outside world should not know. proxy attendance cannot given public
        public event AddTaskEventHandler Add;

        public TaskEventGenerator(ITaskList taskList)
        {
            this.taskList = taskList;
            Console.WriteLine("from TaskEventGenerator added");
        }
        // Invoke the Changed event; called whenever list changes
        protected virtual void OnAdd(TaskAddEventArgs e)
        {
            if (Add != null)
            {
                Add(this, e);
            }
        }
        public void AddTask(TaskItem taskItem)
        {
            taskList.AddTask(taskItem);
            OnAdd(new TaskAddEventArgs(taskItem));
        }
        public void UpdateTask(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        public TaskItem FindTaskByID(long taskID)
        {
            return taskList.FindTaskByID(taskID);
        }

        public long GetCount()
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        public byte[] ExportTask(List<TaskItem> taskItems, Enumerations.ContentType contentType)
        {
            
            throw new NotImplementedException();
        }

        public List<TaskItem> GetAll()
        {
            throw new NotImplementedException();
        }

        //public static void Main()
        //{
        //    ITaskList t = TaskListFactory.GetInstance().CreateList();

        //    ConsoleTaskEventNotifier.TaskEventNotifier(t);

        //    t.AddTask(new TaskItem() { ID = 1 });

        //    System.Threading.Thread.Sleep(10000);

        //}
    }
}
