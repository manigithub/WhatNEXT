using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhatNEXT
{
    public class SimpleTaskList: ITaskList
    {
        private List<TaskItem> taskItems = new List<TaskItem>();

        public List<TaskItem> TaskItems
        {
            get { return taskItems; }
        }

        public void AddTask(TaskItem taskItem)
        {
            if (taskItem != null)
            {
                TaskItems.Add(taskItem);
            }
            else
            {
                throw new ApplicationException("");
            }
        }
        public void UpdateTask(TaskItem taskItemUpdated)
        {
            TaskItem taskItemCurrent = FindTaskByID(taskItemUpdated.ID.ToString());

            if(taskItemCurrent != null)
            {
                TaskItems[TaskItems.IndexOf(taskItemCurrent)] = taskItemUpdated;
            }
            else
            {
                throw new ApplicationException();
            }
        }
        public TaskItem FindTaskByID(string taskID)
        {
            TaskItem taskItem = TaskItems.Find((t) => t.ID == Convert.ToInt64(taskID));

            if (taskItem != null)
                return taskItem;

            throw new ApplicationException();
        }

        public long GetCount()
        {
            return TaskItems.Count;
        }

        private int GetIndexOfTaskItem(TaskItem t)
        {
            return TaskItems.IndexOf(t);
        }
    }
}
