using System;

using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WhatNEXT;


namespace WhatNEXTUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Put it as thread safe collection
        private ConcurrentQueue<TaskItem> taskItemsScheduled = new ConcurrentQueue<TaskItem>();
        private ConcurrentQueue<TaskItem> taskItemsCompleted = new ConcurrentQueue<TaskItem>();
        private WindowState currentWindowState = System.Windows.WindowState.Normal;
        private ConcurrentQueue<long> currentTaskIdShown = new ConcurrentQueue<long>();
        static object locker = new object();
        

        public MainWindow()
        {
            InitializeComponent();
        }
        private void textBoxTaskDetails_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (textBoxTaskDetails.Text.Trim().Length > 0)
                {
                    MessageBox.Show("Task Added." + textBoxTaskDetails.Text);
                    TaskReminder.GetInstance().RemindTask(this.textBoxTaskDetails.Text.Trim());
                    this.textBoxTaskDetails.Text = string.Empty;


                    if (this.taskItemsScheduled.Count > 0)
                    {
                        ShowNextTask();
                    }
                    else
                    {
                        this.WindowState = WindowState.Minimized;
                        currentWindowState = WindowState.Minimized;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter Task Details", "Add Task");
                    textBoxTaskDetails.Focus();
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //currentWindowState = WindowState;
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            currentWindowState = WindowState;
        }
        public void taskScheduler_Schedule(object sender, TaskScheduleEventArgs e)
        {
            taskItemsScheduled.Enqueue(e.Task);

            //the calling thread cannot access this object because a different thread owns it.
            //btnSnooze.IsEnabled = true;

            DelegateUIUpdateToDispatcher();
        }
        private void DelegateUIUpdateToDispatcher()
        {
            if (currentWindowState == WindowState.Minimized && taskItemsScheduled.Count > 0)
            {
                //MessageBox.Show("Invoked from dispatcher ");
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateUIControls(RemindTaskToUser));
            }
            //else if (currentWindowState == WindowState.Normal && taskItemsScheduled.Count > 0 && )
            //{
            //    Dispatcher.BeginInvoke(DispatcherPriority.Normal, () => { MessageBox.Show("Tasks are in Queue. Finish Entering the new Task")});
            //}
        }
        private void ShowNextTask()
        {
            textBoxTaskDetails.Text = "";
            DisableUIControls();
            //Thread.Sleep(2000);
            MessageBox.Show("Loading Next Task...");
            RemindTaskToUser();
        }

        private void RemindTaskToUser()
        {

            TaskItem taskItem = null;
            btnSnooze.IsEnabled = false;
            btnCompleted.IsEnabled = false;            

            if (taskItemsScheduled.TryDequeue(out taskItem) && taskItem != null && taskItem.ID > 0 /* && currentTaskIdShown.TryPeek(out currentTask) && currentTask != taskItem.ID*/)
            {

                taskItemsCompleted.Enqueue(taskItem);
                SetControlsForTaskRemind(taskItem);

            }
        }
        private void SetControlsForTaskRemind(TaskItem taskItem)
        {
            btnSnooze.IsEnabled = true;
            btnCompleted.IsEnabled = true;
            textBoxTaskDetails.IsEnabled = true;
            textBoxTaskDetails.Text = taskItem.Details;
            WindowState = WindowState.Normal;
            currentWindowState = WindowState.Normal;
        }

        private void btnSnooze_Click(object sender, RoutedEventArgs e)
        {
            TaskReminder.GetInstance().RemindTask(textBoxTaskDetails.Text.Trim());

            if (!taskItemsScheduled.IsEmpty)
            {
                ShowNextTask();
            }
            else
            {
                WindowState = WindowState.Minimized;
                currentWindowState = WindowState.Minimized;
                //isDispatcherRequired = true;
            }
        }

        private void DisableUIControls()
        {
            textBoxTaskDetails.IsEnabled = false;
            btnSnooze.IsEnabled = false;
            btnCompleted.IsEnabled = false;
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            TaskReminder.GetInstance().CallMeBack(taskScheduler_Schedule);
        }

        private void btnCompleted_Click(object sender, RoutedEventArgs e)
        {
            TaskItem taskItem = null;
            if (taskItemsCompleted.TryDequeue(out taskItem))
            {
                //Do  completed tasks logging
                textBoxTaskDetails.Text = "";
                if (!taskItemsScheduled.IsEmpty)
                {
                    ShowNextTask();
                }
                else
                {
                    WindowState = WindowState.Minimized;
                    currentWindowState = WindowState.Minimized;
                }

            }
            else
            {
                MessageBox.Show("could not remove task item after completion");
            }
        }
    }
}
