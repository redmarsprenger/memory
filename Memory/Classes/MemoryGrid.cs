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

namespace Memory.Classes
{
    class MemoryGrid
    {

        private Grid grid;
        private int rows;
        private int cols;

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.rows = rows;
            this.cols = cols;
            this.grid = grid;
            InitializeGameGrid(cols, rows);
            AddImages();
        }
        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();
            for (int i = 0; i < (cols * rows); i++)
            {
                int imagenr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/" + imagenr + ".png", UriKind.Relative));
                images.Add(source);
            }

            Random random = new Random();
            for (int i = 0; i < ((cols * rows) / 2); i++)
            {
                int r = random.Next(0, (rows + cols));
                ImageSource temp = images[r];
                images[r] = images[i];
                images[i] = temp;
            }
            return images;
        }
        private void AddImages()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundimage = new Image();
                    backgroundimage.Source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));
                    backgroundimage.Tag = images.First();
                    images.RemoveAt(0);
                    backgroundimage.MouseDown += new MouseButtonEventHandler(cardclick);
                    Grid.SetColumn(backgroundimage, column);
                    Grid.SetRow(backgroundimage, row);
                    grid.Children.Add(backgroundimage);
                }
            }
        }

        private void cardclick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
        }

        private void InitializeGameGrid(int cols, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
    }
}
