using Memory.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScorePage : Page
    {
        /// <summary>
        /// List of Highscore objects
        /// </summary>
        //public List<Highscore> Highscores;
        public HighscoreList highscoreList = HighscoreList.Instance();

        /// <summary>
        /// 
        /// </summary>
        public HighScorePage()
        {
            InitializeComponent();
            //mock data
//            highscoreList.AddHighscore(new Highscore("Johan", 24, DateTime.Now));
//            highscoreList.AddHighscore(new Highscore("Freek", 7, DateTime.Now));
//            highscoreList.AddHighscore(new Highscore("Anouk", 55, DateTime.Now));
//            highscoreList.AddHighscore(new Highscore("Emiel", 65, DateTime.Now));
//            highscoreList.AddHighscore(new Highscore("Mirte", 33, DateTime.Now));

//            highscoreList.Save();

            highscoreList.Load();
            //Binds the data to the table on the page
            HighscoreTable.ItemsSource = highscoreList.GetList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BacktoStartbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }
    }
}
