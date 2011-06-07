using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    class TaskTextParser: ITaskParser
    {
        private string[] timeSeparatorKeyWords = {"before", "after", "at"};
        private TaskItem taskItem = new TaskItem();
        //after 20 mins
        //before ---

        public TaskItem Parse(string taskDetails)
        {
            taskItem.ID = DateTime.Now.ToFileTimeUtc();

            int indexTaskSplitter = taskDetails.IndexOf("after");

            if(indexTaskSplitter != -1)
            {
                taskItem.Details = taskDetails.Substring(0, indexTaskSplitter).Trim();
                taskItem.TimeReminder = Convert.ToInt32(taskDetails.Substring(indexTaskSplitter + "after".Length).Trim().TrimEnd('s'))*1000;
                
            }
            return taskItem;
        }
    }
}
