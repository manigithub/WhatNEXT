using System;
using System.Collections.Generic;
using System.Text;
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
using WhatNEXT;


namespace WhatNEXTUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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
    }
}
