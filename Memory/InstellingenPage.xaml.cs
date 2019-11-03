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
            "pack://application:,,,/Memory;component/Resources/themes/Cards/achterkant.png",
            "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png",
        };

        string[] themeNames = new string[]
        {
            "Cards",
            "Sport",
        };

        ImageSourceConverter converter = new ImageSourceConverter();

        public InstellingenPage()
        {
            InitializeComponent();
            //Laad alle settings in vanuit de opgeslagen settings.
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

            Savetheme(lblActiveTheme.Content, imgTheme.Source.ToString());
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = Array.FindIndex(theme, row => row.Contains(imgTheme.Source.ToString()));
            string nextElement = GetNextElement(theme, currentIndex);

            imgTheme.Source = new BitmapImage(new Uri(nextElement));
            lblActiveTheme.Content = themeNames[GetNextIndex(theme, currentIndex)];

            Savetheme(lblActiveTheme.Content, imgTheme.Source.ToString());
        }

        private void Savetheme(object ThemeName, string theme)
        {
            Settings.Default["ThemeName"] = ThemeName;
            Settings.Default["Theme"] = theme;
            Settings.Default.Save();
        }

        private string GetNextElement(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == strArray.Length - 1)
                index = 0;

            else
                index++;

            return strArray[index];
        }

        private string GetPreviousElement(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == 0)
                index = strArray.Length - 1;

            else
                index--;

            return strArray[index];
        }

        private int GetNextIndex(string[] strArray, int index)
        {
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == strArray.Length - 1)
                index = 0;

            else
                index++;

            return index;
        }

        private int GetPreviousIndex(string[] strArray, int index)
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
            SaveSettingsBool("Music");
            MainWindow main = new MainWindow();
            main.toggleMusic();
        }

        private void ToggleSwitchSound_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingsBool("Sound");
        }

        private void SaveSettingsBool(string setting)
        {
            if ((bool)Settings.Default[setting])
            {
                Settings.Default[setting] = false;
            }
            else
            {
                Settings.Default[setting] = true;
            }
            Settings.Default.Save();
        }

        private void ToggleSwitchMusic_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
