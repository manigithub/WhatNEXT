using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;


namespace WhatNEXT
{
    public class SimpleTaskList : ITaskList     
    {
        private List<TaskItem> taskItems = new List<TaskItem>();

        public List<TaskItem> TaskItems
        {
            get { return taskItems; }
        }

        public SimpleTaskList()
        {
            Console.WriteLine("from simpletasklist added");
        }

        public void AddTask(TaskItem taskItem)
        {
            

            if (taskItem == null || null != FindTaskByID(taskItem.ID))
            {
                throw new ApplicationException("");
            }
            TaskItems.Add(taskItem);

        }
        public void UpdateTask(TaskItem taskItemUpdated)
        {
            TaskItem taskItemCurrent = FindTaskByID(taskItemUpdated.ID);

            if (taskItemCurrent != null)
            {
                TaskItems[TaskItems.IndexOf(taskItemCurrent)] = taskItemUpdated;
            }
            else
            {
                throw new ApplicationException();
            }
        }
        public TaskItem FindTaskByID(Int64 taskID)
        {

            return TaskItems.Find((t) => t.ID == taskID);

        }
        public long GetCount()
        {
            return TaskItems.Count;
        }

        private int GetIndexOfTaskItem(TaskItem t)
        {
            return TaskItems.IndexOf(t);
        }

        public override string ToString()
        {
            return base.ToString() + " SimpleTaskList";
        }

        public void DeleteTask(TaskItem taskItem)
        {

            bool isTaskItemRemoved = TaskItems.Remove(taskItem);

            if (isTaskItemRemoved == false)
            {
                throw new ApplicationException();
            }
        }

        public byte[] ExportTask(List<TaskItem> taskList, Enumerations.ContentType contentType)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<TaskItem>));

            MemoryStream memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, taskList);

            return memoryStream.GetBuffer();

        }

        public List<TaskItem> GetAll()
        {
            return TaskItems;
        }
    }
}
