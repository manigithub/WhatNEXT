﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
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
        private Boolean isDispatcherRequired = true;
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

            //textBoxTaskDetails.Text = taskItem.Details;
            //this.WindowState = WindowState.Normal;

            DelegateUIUpdateToDispatcher();
        }
        private void DelegateUIUpdateToDispatcher()
        {
            lock (locker)
            {
                if (isDispatcherRequired)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new UpdateUIControls(UpdateUI));
                }
                isDispatcherRequired = false;
            }

           
        }

        private void UpdateUI()
        {
            TaskItem taskItem = null;
            btnSnooze.IsEnabled = false;
            btnCompleted.IsEnabled = false;

            while (taskItemsScheduled.TryDequeue(out taskItem) && taskItem != null && taskItem.ID > 0)
            {
                taskItemsCompleted.Enqueue(taskItem);
                btnSnooze.IsEnabled = true;
                btnCompleted.IsEnabled = true;
                textBoxTaskDetails.IsEnabled = true;
                textBoxTaskDetails.Text = taskItem.Details;
                WindowState = WindowState.Normal;
                taskItem = null;
            }
        }

        private void btnSnooze_Click(object sender, RoutedEventArgs e)
        {
            TaskReminder.GetInstance().RemindTask(textBoxTaskDetails.Text.Trim());

            lock (locker)
            {
                if (taskItemsScheduled.IsEmpty)
                {
                    WindowState = WindowState.Minimized;
                    isDispatcherRequired = true;
                }
                else
                {
                    textBoxTaskDetails.Text = "Loading next task....wait...";
                    DisableUIControls();
                    //MessageBox.Show("NExt Task comes here");
                    UpdateUI();
                }
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
                textBoxTaskDetails.Text = string.Empty;
                WindowState = WindowState.Minimized;
            }
            else
            {

                MessageBox.Show("could not remove task item after completion");
            }

        }
    }
}
