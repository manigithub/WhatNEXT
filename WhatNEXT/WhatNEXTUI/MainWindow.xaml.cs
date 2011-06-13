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
                    this.WindowState = WindowState.Minimized;
                    currentWindowState = WindowState.Minimized;
                    TaskReminder.GetInstance().RemindTask(this.textBoxTaskDetails.Text.Trim());


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
            //this.textBoxTaskDetails.Text = string.Empty;
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            //this.textBoxTaskDetails.Text = string.Empty;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

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
                MessageBox.Show("Invoked from dispatcher ");
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateUIControls(RemindTaskToUser));
            }
        }

        private void RemindTaskToUser()
        {

            TaskItem taskItem = null;
            btnSnooze.IsEnabled = false;
            btnCompleted.IsEnabled = false;
            long currentTask = -1;

            if (taskItemsScheduled.TryDequeue(out taskItem) && taskItem != null && taskItem.ID > 0 /* && currentTaskIdShown.TryPeek(out currentTask) && currentTask != taskItem.ID*/)
            {
                
                taskItemsCompleted.Enqueue(taskItem);
                btnSnooze.IsEnabled = true;
                btnCompleted.IsEnabled = true;
                textBoxTaskDetails.IsEnabled = true;
                textBoxTaskDetails.Text = taskItem.Details;
                WindowState = WindowState.Normal;
                currentWindowState = WindowState.Normal;
            }
        }

        private void btnSnooze_Click(object sender, RoutedEventArgs e)
        {
            TaskReminder.GetInstance().RemindTask(textBoxTaskDetails.Text.Trim());

            if (!taskItemsScheduled.IsEmpty)
            {
                textBoxTaskDetails.Text = "";
                DisableUIControls();
                //Thread.Sleep(2000);
                MessageBox.Show("Loading Next Task...");
                RemindTaskToUser();
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
                    DisableUIControls();
                    //Thread.Sleep(2000)
                    MessageBox.Show("Loading Next Task...");
                    RemindTaskToUser();
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
