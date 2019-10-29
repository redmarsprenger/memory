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

        private void SpelSelectiebtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("SpelSelectiePage.xaml", UriKind.Relative));
        }

        private void BacktoStartbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }

        // this is the button to select a gamefile so you can replay old games THIS IS STILL WIP furthermore this brings you for now to the GamePage
        private void SpelSelectFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var gamePage = new GamePage(this, "F");
            NavigationService.Navigate(gamePage);
        }
    }
}
