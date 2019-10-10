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
        public List<Highscore> Highscores;

        public HighScorePage()
        {
            InitializeComponent();
            //creates the typed List()
            Highscores = new List<Highscore>();
            //mock data
            Highscores.Add(new Highscore("Freek", 7, DateTime.Now));
            //Binds the data to the table on the page
            HighscoreTable.ItemsSource = Highscores;
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }
    }
}
