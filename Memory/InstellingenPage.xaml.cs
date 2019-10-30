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

        /// <summary>
        /// 
        /// </summary>
        public InstellingenPage()
        {
            InitializeComponent();
            //Laad alle settings in vanuit de opgeslagen settings.
            ToggleSwitchMusic.IsChecked = (bool)Settings.Default["Music"];
            ToggleSwitchSound.IsChecked = (bool)Settings.Default["Sound"];
            lblActiveTheme.Content = (string)Settings.Default["ThemeName"];
            imgTheme.Source = (ImageSource)converter.ConvertFromString((string)Settings.Default["Theme"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("WelkomPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = Array.FindIndex(theme, row => row.Contains(imgTheme.Source.ToString()));
            string nextElement = GetPreviousElement(theme, currentIndex);

            imgTheme.Source = new BitmapImage(new Uri(nextElement));
            lblActiveTheme.Content = themeNames[GetPreviousIndex(theme, currentIndex)];

            Savetheme(lblActiveTheme.Content, imgTheme.Source.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = Array.FindIndex(theme, row => row.Contains(imgTheme.Source.ToString()));
            string nextElement = GetNextElement(theme, currentIndex);

            imgTheme.Source = new BitmapImage(new Uri(nextElement));
            lblActiveTheme.Content = themeNames[GetNextIndex(theme, currentIndex)];

            Savetheme(lblActiveTheme.Content, imgTheme.Source.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ThemeName"></param>
        /// <param name="theme"></param>
        private void Savetheme(object ThemeName, string theme)
        {
            Settings.Default["ThemeName"] = ThemeName;
            Settings.Default["Theme"] = theme;
            Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchMusic_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingsBool("Music");
            MainWindow main = new MainWindow();
            main.toggleMusic();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchSound_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingsBool("Sound");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
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
    }
}
