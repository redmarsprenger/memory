﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Memory.Classes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for SpelPage.xaml
    /// </summary>
    public partial class SpelPage : Page
    {
        public SpelPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This will display the interaction of going back to the starting page.//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one.//
            NavigationService.Navigate(new WelkomPage());
        }

        /// <summary>
        /// This will display the interaction of going back to the starting page.//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one.//
            NavigationService.Navigate(new SpelSelectiePage());
        }

        /// <summary>
        /// Loads saved game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GamePage(true));
        }
    }
}
