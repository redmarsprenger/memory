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
        public InstellingenPage()
        {
            InitializeComponent();
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
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = Array.FindIndex(theme, row => row.Contains(imgTheme.Source.ToString()));
            string nextElement = GetNextElement(theme, currentIndex);
            imgTheme.Source = new BitmapImage(new Uri(nextElement));
            lblActiveTheme.Content = themeNames[GetNextElementIndex(theme, currentIndex)];
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
    }
}
