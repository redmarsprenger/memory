using Memory.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            //sorts the table with highest score first
            HighscoreTable.Items.SortDescriptions.Add(new SortDescription("Score", ListSortDirection.Descending));

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
            NavigationService.Navigate(new WelkomPage());
        }
    }
}
