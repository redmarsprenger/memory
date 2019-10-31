using System;
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
using Memory.Classes;

namespace Memory
{
    public partial class pausepage : Page
    {
        private GamePage gamePage;

        /// <summary>
        /// InitializeComponents and saves the gamePage so it can be resumed.
        /// </summary>
        /// <param name="gamePage"></param>
        public pausepage(GamePage gamePage)
        {
            InitializeComponent();

            this.gamePage = gamePage;
        }
        
        /// <summary>
        /// Resumes game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hervatbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(gamePage);
        }

        /// <summary>
        /// Saves the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opslaanbtn_Click(object sender, RoutedEventArgs e)
        {

            string gamePageString = XamlWriter.Save(gamePage);
            //            ObjectSterializer objSterializer = new ObjectSterializer();
            //            objSterializer.SerializeObject(gamePageString, "SavedGame");

            File.WriteAllText("SavedGame", gamePageString);

            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService.Navigate(new WelkomPage());
        }

        /// <summary>
        /// Exits the game without saving
        /// Redirects to WelkomPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quitbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WelkomPage());
        }
    }
}