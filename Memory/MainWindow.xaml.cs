using System;
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
using System.Media;
using System.Threading;
using Memory.Classes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        private object soundLocation;

        private HighscoreList highscoreList = HighscoreList.Instance();

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            highscoreList.Load();

            if ((string)Settings.Default["ThemeName"] == "blue")
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
            }

            string filename = "../../Resources/music/background_music.wav";
            string path = System.IO.Path.GetFullPath(filename);
            string url = new Uri(path).AbsoluteUri;
            sp.SoundLocation = url;

            toggleMusic();

            var welcomeUri = new Uri("WelkomPage.xaml", UriKind.Relative);
            frmMainContent.Source = welcomeUri; // initialize the beginner frame with the "WelkomPage" view
        }

        /// <summary>
        /// 
        /// </summary>
        public void toggleMusic()
        {
            //Loops music in background if music setting is on
            if ((bool)Settings.Default["Music"])
            {
                Task.Factory.StartNew(() => { sp.PlayLooping(); });
            }
            else
            {
                sp.Stop();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //highscoreList.Save();
            sp.Stop();
            Application.Current.Exit
        }
    }
}
