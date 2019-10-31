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
        /// Loads highscores
        /// Makes sure theme works
        /// Toggles the background music on or off depending on the settings
        /// Loads WelkomPage.xaml
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            highscoreList.Load();

            // The blue theme is cashed somewhere. We don't know where but this way it gets replaced with the Sport theme if it is active
            if ((string)Settings.Default["ThemeName"] == "blue")
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
            }

            // Defines the background music
            string filename = "../../Resources/music/background_music.wav";
            string path = System.IO.Path.GetFullPath(filename);
            string url = new Uri(path).AbsoluteUri;
            sp.SoundLocation = url;

            toggleMusic();

            // initialize the beginner frame with the "WelkomPage" view//
            var welcomeUri = new Uri("WelkomPage.xaml", UriKind.Relative);
            frmMainContent.Source = welcomeUri; 
        }

        /// <summary>
        /// Toggles background music on and off
        /// </summary>
        public void toggleMusic()
        {
            //Loops music in background, in separate thread,  if music setting is on
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
        /// Stop background music
        /// Makes sure the application completely shuts down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainWindow_Closed(object sender, EventArgs e)
        {
            sp.Stop();
            Application.Current.Shutdown();
        }
    }
}
