using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public static class TaskReminderUtility
    {
        public static void RemindTask(string taskDetails)
        {

            ITaskParser taskParser = TaskListFactory.GetInstance().CreateTaskParser();

            taskParser.Parse("");
        }
    }
}
