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
using Memory.Properties;

namespace Memory
{
    /// <summary>
    /// Interaction logic for InstellingenPage.xaml
    /// </summary>
    public partial class InstellingenPage : Page
    {
        string[] theme = new string[]
        {
            "pack://application:,,,/Memory;component/Resources/memo.png",
            "pack://application:,,,/Memory;component/Resources/memo2.png",
            "pack://application:,,,/Memory;component/Resources/memo3.png",
        };
        string[] themeNames = new string[]
        {
            "Blauw",
            "Rood",
            "Geel",
        };
        ImageSourceConverter converter = new ImageSourceConverter();
        public InstellingenPage()
        {
            InitializeComponent();
            ToggleSwitchMusic.IsChecked = (bool)Settings.Default["Music"];
            ToggleSwitchSound.IsChecked = (bool)Settings.Default["Sound"];
            lblActiveTheme.Content = (string)Settings.Default["ThemeName"];
            imgTheme.Source = (ImageSource)converter.ConvertFromString((string)Settings.Default["Theme"]);
        }
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = Array.FindIndex(theme, row => row.Contains(imgTheme.Source.ToString()));
            string nextElement = GetPreviousElement(theme, currentIndex);
            imgTheme.Source = new BitmapImage(new Uri(nextElement));
            lblActiveTheme.Content = themeNames[GetPreviousIndex(theme, currentIndex)];
            Settings.Default["ThemeName"] = lblActiveTheme.Content;
            Settings.Default["Theme"] = imgTheme.Source;
            Settings.Default.Save();
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = Array.FindIndex(theme, row => row.Contains(imgTheme.Source.ToString()));
            string nextElement = GetNextElement(theme, currentIndex);
            imgTheme.Source = new BitmapImage(new Uri(nextElement));
            lblActiveTheme.Content = themeNames[GetNextElementIndex(theme, currentIndex)];
            Settings.Default["ThemeName"] = lblActiveTheme.Content;
            Settings.Default["Theme"] = nextElement;
            Settings.Default.Save();
        }

        public string GetNextElement(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == strArray.Length - 1)
                index = 0;

            else
                index++;

            return strArray[index];
        }

        public string GetPreviousElement(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == 0)
                index = strArray.Length - 1;

            else
                index--;

            return strArray[index];
        }

        public int GetNextElementIndex(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == strArray.Length - 1)
                index = 0;

            else
                index++;

            return index;
        }

        public int GetPreviousIndex(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == 0)
                index = strArray.Length - 1;

            else
                index--;

            return index;
        }

        private void ToggleSwitchMusic_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Settings.Default["Music"])
            {
                Settings.Default["Music"] = false;
            }
            else
            {
                Settings.Default["Music"] = true;
            }
            Settings.Default.Save();
        }

        private void ToggleSwitchSound_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Settings.Default["Sound"])
            {
                Settings.Default["Sound"] = false;
            }
            else
            {
                Settings.Default["Sound"] = true;
            }
            Settings.Default.Save();
        }
    }
}
