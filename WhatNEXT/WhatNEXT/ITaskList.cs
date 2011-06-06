using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WhatNEXT
{
    public interface ITaskList
    {
        void AddTask(TaskItem taskItem);
        void UpdateTask(TaskItem taskItem);
        TaskItem FindTaskByID(Int64 taskID);
        Int64 GetCount();
        void DeleteTask(TaskItem taskItem);
        byte[] ExportTask(List<TaskItem> taskList, Enumerations.ContentType contentType);
        List<TaskItem> GetAll();
    }
}
