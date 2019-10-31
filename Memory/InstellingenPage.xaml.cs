using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using Path = System.IO.Path;

namespace Memory
{
    /// <summary>
    /// Interaction logic for InstellingenPage.xaml
    /// </summary>
    public partial class InstellingenPage : Page
    {
        string[] theme = new string[]
        {
        };

        string[] themeNames = new string[]
        {
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
            try
            {
                imgTheme.Source = (ImageSource)converter.ConvertFromString((string)Settings.Default["Theme"]);
            }
            catch
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
                imgTheme.Source = (ImageSource)converter.ConvertFromString((string)Settings.Default["Theme"]);
            }
            imgTheme.Source = (ImageSource)converter.ConvertFromString((string)Settings.Default["Theme"]);


            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            string baseFolder = Path.Combine(projectPath, "Resources\\themes");



            List<string> themeList = new List<string>();
            List<string> namesList = new List<string>();

            string[] employeeFolders = Directory.GetDirectories(baseFolder);
            string imgName = "achterkant.png";
            foreach (var folderName in employeeFolders)
            {
                var path = System.IO.Path.Combine(folderName, imgName);
                if (File.Exists(path))
                {
                    themeList.Add(path);
                }
            }

            foreach (var folderName in employeeFolders)
            {
                var path = System.IO.Path.Combine(folderName);
                if (Directory.Exists(path))
                {
                    string input = path;
                    int index = input.IndexOf("themes\\");
                    if (index > 0)
                        input = input.Substring(index+7);

                    namesList.Add(input);
                }
            }

            theme = themeList.ToArray();
            themeNames = namesList.ToArray();
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
            int currentIndex = Array.FindIndex(themeShortner(), row => row.Contains(imgThemeShortner()));
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

            int currentIndex = Array.FindIndex(themeShortner(), row => row.Contains(imgThemeShortner()));
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
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
            }

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
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
            }

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
            GamePage gamePage = new GamePage();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string imgThemeShortner()
        {
            var imgThemeSource = imgTheme.Source;
            Uri imgThemeSourceUri = new Uri(imgThemeSource.ToString());
            string imgThemeString = imgThemeSourceUri.AbsolutePath;
            int index = imgThemeString.IndexOf("themes/");
            if (index > 0)
                imgThemeString = imgThemeString.Substring(index + 7);

            return imgThemeString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string[] themeShortner()
        {
            string[] themes;
            List<string> listThemes = new List<string>();
            foreach (var th in theme)
            {
                string inputs;
                inputs = th.Replace('\\', '/');
                int indexs = inputs.IndexOf("themes/");
                if (indexs > 0)
                    inputs = inputs.Substring(indexs + 7);

                listThemes.Add(inputs);
            }
            themes = listThemes.ToArray();

            return themes;
        }
    }
}
