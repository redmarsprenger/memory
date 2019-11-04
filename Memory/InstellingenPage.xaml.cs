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
        /// Sets the settings correct for the user to navigate trough
        /// Loads in all the themes 
        /// </summary>
        public InstellingenPage()
        {
            InitializeComponent();

            //Laad alle settings in vanuit de opgeslagen settings.
            ToggleSwitchMusic.IsChecked = (bool)Settings.Default["Music"];
            ToggleSwitchSound.IsChecked = (bool)Settings.Default["Sound"];
            lblActiveTheme.Content = (string)Settings.Default["ThemeName"];
            // If saved theme doesn't exist load the "Sport" theme
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

            //Gets the project path needed for getting the themes
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string baseFolder = Path.Combine(projectPath, "Resources\\themes");



            List<string> themeList = new List<string>();
            List<string> namesList = new List<string>();

            //Fills the themeList
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

            //Fills the namesList
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
        /// Returns to WelkomPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            // through the course of clicking the button the navigation system switches the current frame uri to the new one//
            NavigationService.Navigate(new WelkomPage());
        }

        /// <summary>
        /// Gets previous theme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            // Find the current index
            int currentIndex = Array.FindIndex(themeShortner(), row => row.Contains(imgThemeShortner()));

            // Find the previous elements and sets them in imgTheme and lblActiveTheme
            imgTheme.Source = new BitmapImage(new Uri(GetPreviousElement(theme, currentIndex)));
            lblActiveTheme.Content = themeNames[GetPreviousIndex(theme, currentIndex)];

            //Saves the theme
            Savetheme(lblActiveTheme.Content, imgTheme.Source.ToString());
        }

        /// <summary>
        /// Gets next theme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            // Find the current index
            int currentIndex = Array.FindIndex(themeShortner(), row => row.Contains(imgThemeShortner()));

            // Find the next elements and sets them in imgTheme and lblActiveTheme
            imgTheme.Source = new BitmapImage(new Uri(GetNextElement(theme, currentIndex)));
            lblActiveTheme.Content = themeNames[GetNextIndex(theme, currentIndex)];

            //Saves the theme
            Savetheme(lblActiveTheme.Content, imgTheme.Source.ToString());
        }

        /// <summary>
        /// Saves the ThemeName and Theme
        /// </summary>
        /// <param name="ThemeName"></param>
        /// <param name="theme"></param>
        private void Savetheme(object ThemeName, string theme)
        {
            try
            {
                Settings.Default["ThemeName"] = ThemeName;
                Settings.Default["Theme"] = theme;
                Settings.Default.Save();
            }
            // It should be impossible to not be able to save a given ThemeName and theme but just in case...
            catch
            { }
        }

        /// <summary>
        /// Gets the next element of the given array and index.
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetNextElement(string[] strArray, int index)
        {
            // If the index doesn't exist trow error
            if ((index > strArray.Length - 1) || (index < 0))
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
            }

            // If the index is 0 return the last index
            if (index == strArray.Length - 1)
            {
                index = 0;
            }
            // Next index
            else
            {
                index++;
            }

            return strArray[index];
        }

        /// <summary>
        /// Gets the previous element of the given array and index.
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetPreviousElement(string[] strArray, int index)
        {
            // If the index doesn't exist load in the "Sport" theme
            if ((index > strArray.Length - 1) || (index < 0))
            {
                Settings.Default["ThemeName"] = "Sport";
                Settings.Default["Theme"] = "pack://application:,,,/Memory;component/Resources/themes/Sport/achterkant.png";
                Settings.Default.Save();
            }

            // if the index is 0 return the last index
            if (index == 0)
            {
                index = strArray.Length - 1;
            }
            // Previous index
            else
            {
                index--;
            }

            return strArray[index];
        }

        /// <summary>
        /// Gets the next index of the given array and index.
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetNextIndex(string[] strArray, int index)
        {
            // If the index doesn't exist trow error
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            // If the index is 0 return the last index
            if (index == strArray.Length - 1) { 
                index = 0;
            }
            // Next index
            else
            {
                index++;
            }

            return index;
        }

        /// <summary>
        /// Gets the previous index of the given array and index.
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetPreviousIndex(string[] strArray, int index)
        {
            // If the index doesn't exist trow error
            if ((index > strArray.Length - 1) || (index < 0))
                throw new Exception("Invalid index");

            // if the index is 0 return the last index
            if (index == 0)
            {
                index = strArray.Length - 1;
            }
            // Previous index
            else
            {
                index--;
            }

            return index;
        }

        /// <summary>
        /// Toggles the music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchMusic_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingsBool("Music");
            // Turns the music on or off located in the mainWindow
            MainWindow main = new MainWindow();
            main.toggleMusic();
        }

        /// <summary>
        /// Toggles the sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchSound_Click(object sender, RoutedEventArgs e)
        {
            SaveSettingsBool("Sound");
        }

        /// <summary>
        /// Saves the given settings bool if it exists
        /// </summary>
        /// <param name="setting"></param>
        private void SaveSettingsBool(string setting)
        {
            if ((bool)Settings.Default[setting])
            {
                Settings.Default[setting] = false;
            }
            // Needs extra if statement to prevent trying to change nonexisting setting.
            else if(!(bool)Settings.Default[setting])
            {
                Settings.Default[setting] = true;
            }
            Settings.Default.Save();
        }

        /// <summary>
        /// Shortens the "imgTheme" string to the theme name folder + achterkant.png
        /// </summary>
        /// <returns></returns>
        private string imgThemeShortner()
        {
            //Gets the correct path
            var imgThemeSource = imgTheme.Source;
            Uri imgThemeSourceUri = new Uri(imgThemeSource.ToString());
            string imgThemeString = imgThemeSourceUri.AbsolutePath;
            int index = imgThemeString.IndexOf("themes/");
            //Shortens the imgTheme by using Substring
            if (index > 0)
                imgThemeString = imgThemeString.Substring(index + 7);

            return imgThemeString;
        }

        /// <summary>
        /// Shortens the "theme" string[] to the theme name folder + achterkant.png
        /// </summary>
        /// <returns></returns>
        private string[] themeShortner()
        {
            string[] themes;
            List<string> listThemes = new List<string>();
            //Loops trough each theme[]
            foreach (var th in theme)
            {
                //Replace \\ with / to make it the same as the imTheme
                string inputs;
                inputs = th.Replace('\\', '/');
                int indexs = inputs.IndexOf("themes/");
                //Shortens the listThemes by using Substring
                if (indexs > 0)
                    inputs = inputs.Substring(indexs + 7);

                listThemes.Add(inputs);
            }
            //List to array so it can be returned
            themes = listThemes.ToArray();

            return themes;
        }
    }
}
