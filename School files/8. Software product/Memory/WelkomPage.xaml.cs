﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for WelkomPage.xaml
    /// </summary>
    public partial class WelkomPage : Page
    {
        /// <summary>
        /// InitializeComponents
        /// </summary>
        public WelkomPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Navigates to InstellingPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instellingenbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService.Navigate(new InstellingenPage());
        }
        
        /// <summary>
        /// Navigates to SpelPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService.Navigate(new SpelPage());
        }

        /// <summary>
        /// Navigates to HighScorePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Highscorebtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService.Navigate(new HighScorePage());
        }
    }
}
