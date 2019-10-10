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

namespace Memory
{
    /// <summary>
    /// Interaction logic for PauzePage.xaml
    /// </summary>
    public partial class PauzePage : Page
    {
        public PauzePage()
        {
            InitializeComponent();
        }

        // This is the button to Resume your game with, THIS IS STILL WIP furthermore this will bring you back to the GamePage//
        private void VerderGaanbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("PauzePage.xaml", UriKind.Relative));
        }

        // This is the button to exit the game with, THIS IS STILL WIP furthermore this will bring you back to the WelkomPage//
        private void ExitGamebtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }

        // this is the button to save the game, THIS IS STILL WIP furthermore this will bring you back to the Welkompage
        private void SaveGamebtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }
    }
}
