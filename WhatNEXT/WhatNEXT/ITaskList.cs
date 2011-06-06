using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public interface ITaskList
    {
         void AddTask(TaskItem taskItem);
         void UpdateTask(TaskItem taskItem);
         TaskItem FindTaskByID(string taskID);
         long GetCount();

    }
}
