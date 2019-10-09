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
    /// Interaction logic for WelkomPage.xaml
    /// </summary>
    public partial class WelkomPage : Page
    {
        public WelkomPage()
        {
            InitializeComponent();
        }

        private void Instellingenbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("InstellingenPage.xaml", UriKind.Relative));
        }

        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Highscorebtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
