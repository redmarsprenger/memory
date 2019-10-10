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
    public partial class pausepage : Page
    {
        public pausepage()
        {
            InitializeComponent();

        }
        
        // This is the button to Resume your game with, THIS IS STILL WIP furthermore this will bring you back to the GamePage//
        private void Hervatbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("gamepage.xaml", UriKind.Relative));
        }

        // this is the button to save the game, THIS IS STILL WIP furthermore this will bring you back to the Welkompage
        private void Opslaanbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Welkompage.xaml", UriKind.Relative));
        }

        // This is the button to exit the game with, THIS IS STILL WIP furthermore this will bring you back to the WelkomPage//
        private void Quitbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Welkompage.xaml", UriKind.Relative));
        }
    }
}