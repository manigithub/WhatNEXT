using System;
using System.Collections.Generic;
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
        private List<TaskItem> taskItemsScheduled = new List<TaskItem>();

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
            taskItemsScheduled.Add(e.Task);
            //the calling thread cannot access this object because a different thread owns it.
            //btnSnooze.IsEnabled = true;

            //textBoxTaskDetails.Text = taskItem.Details;
            //this.WindowState = WindowState.Normal;

            // Place delegate on the Dispatcher.
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new UpdateUIControls(UpdateUI));
        }

        private void UpdateUI()
        {
            bool isFirstScheduledItem = true;
         
                //if (isFirstScheduledItem == false)
                //{
                //    this.IsEnabled = false;
                //    Thread.Sleep(2000);
                //    this.IsEnabled = true;
                //}
                btnSnooze.IsEnabled = true;
                textBoxTaskDetails.Text = taskItemsScheduled[0].Details;
                this.WindowState = WindowState.Normal;
                //isFirstScheduledItem = false;
           
        }

        private void btnSnooze_Click(object sender, RoutedEventArgs e)
        {   
            TaskReminder.GetInstance().RemindTask(this.taskItemsScheduled[0]);
            this.taskItemsScheduled.RemoveAt(0);
            TaskReminder.GetInstance().RemindTask(this.textBoxTaskDetails.Text.Trim());
            if (taskItemsScheduled.Count > 0)
            {
                btnSnooze.IsEnabled = true;
                textBoxTaskDetails.Text = taskItemsScheduled[0].Details;
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }

        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            TaskReminder.GetInstance().CallMeBack(taskScheduler_Schedule);
        }

        private void btnCompleted_Click(object sender, RoutedEventArgs e)
        {
            this.taskItemsScheduled.RemoveAt(0);
        }
    }
}
