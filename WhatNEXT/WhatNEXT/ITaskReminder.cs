using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNEXT
{
    public interface ITaskReminder
    {
        void RemindTask(string taskDetails);
        void RemindTask(TaskItem taskItem);
        void CallMeBack(RemindMe CallBackMethod);
    }
}
