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
        /// <summary>
        /// 
        /// </summary>
        public SpelSelectiePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BacktoSpelbtn_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService.Navigate(new SpelPage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginSpelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Textboxspeler2.IsEnabled)
            {
                NavigationService.Navigate(new GamePage(textboxspeler1.Text));
            }
            else
            {
                NavigationService.Navigate(new GamePage(textboxspeler1.Text, Textboxspeler2.Text));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchSpeler_Checked(object sender, RoutedEventArgs e)
        {
            if (ToggleSwitchSpeler.IsChecked == true)
            {
                Textboxspeler2.IsEnabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchSpeler_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ToggleSwitchSpeler.IsChecked == false)
            {
                Textboxspeler2.IsEnabled = false;
            }

        }
    }

}

