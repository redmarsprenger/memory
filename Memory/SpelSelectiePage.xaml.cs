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
    /// Interaction logic for SpelSelectiePage.xaml
    /// </summary>
    public partial class SpelSelectiePage : Page
    {
        public SpelSelectiePage()
        {
            InitializeComponent();
        }

        private void BacktoSpelbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            var SpelPage = new SpelPage();
            NavigationService.Navigate(SpelPage);
        }

        private void BeginSpelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Textboxspeler2.IsEnabled)
            {
                var GamePage = new GamePage(textboxspeler1.Text);
                NavigationService.Navigate(GamePage);
            }
            else
            {
                var GamePage = new GamePage(textboxspeler1.Text, Textboxspeler2.Text);
                NavigationService.Navigate(GamePage);
            }

        }

        private void ToggleSwitchSpeler_Checked(object sender, RoutedEventArgs e)
        {
            if (ToggleSwitchSpeler.IsChecked == true)
            {
                Textboxspeler2.IsEnabled = true;
            }
        }

        private void ToggleSwitchSpeler_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ToggleSwitchSpeler.IsChecked == false)
            {
                Textboxspeler2.IsEnabled = false;
            }

        }
    }

}

