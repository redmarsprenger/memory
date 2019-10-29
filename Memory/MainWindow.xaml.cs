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
using Memory.Properties;
using System.IO;


namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        private object soundLocation;

        public MainWindow()
        {
            InitializeComponent();
            
            string filename = "../../Resources/music/background_music.wav";
            string path = System.IO.Path.GetFullPath(filename);
            string url = new Uri(path).AbsoluteUri;
            sp.SoundLocation = url;

            toggleMusic();

            var welcomeUri = new Uri("WelkomPage.xaml", UriKind.Relative);
            frmMainContent.Source = welcomeUri; // initialize the beginner frame with the "WelkomPage" view
        }

        public void toggleMusic()
        {
            //Loops music in background if music setting is on
            if ((bool)Settings.Default["Music"])
            {
                sp.Play();
            }
            else
            {
                sp.Stop();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sp.Stop();
        }
    }
}
