using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using Memory.Properties;

namespace Memory.Classes
{
    class MemoryGrid
    {

        private Grid grid;
        private int rows;
        private int cols;
        private int cardsOpen;
        private Image firstCard;
        private Image secondCard;
        private List<ImageSource> images = new List<ImageSource>();

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.rows = rows;
            this.cols = cols;
            this.grid = grid;
            InitializeGameGrid(cols, rows);
            AddImages();
        }

        public MemoryGrid(String savedGrid)
        {
            StringReader stringReader = new StringReader(savedGrid);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid readerLoadGrid = (Grid)XamlReader.Load(xmlReader);
            this.grid = readerLoadGrid;
        }
        public Grid getGrid()
        {
            return grid;
        }

        private List<ImageSource> GetImagesList()
        {
            for (int i = 0; i < (cols * rows); i++)
            {
                int imagenr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/" + imagenr + ".png", UriKind.Relative));
                images.Add(source);
            }

//            Random random = new Random();
//            for (int i = 0; i < ((cols * rows) / 2); i++)
//            {
//                int r = random.Next(0, (rows + cols));
//                ImageSource temp = images[r];
//                images[r] = images[i];
//                images[i] = temp;
//            }
            return images;
        }
        private void AddImages()
        {
            images = GetImagesList();
            int imageNumber = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundimage = new Image();
                    backgroundimage.Source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));
                    backgroundimage.Tag = images[imageNumber];
                    imageNumber++;
                    backgroundimage.DataContext = backgroundimage.Source;
//                    images.RemoveAt(0);
                    backgroundimage.MouseDown += new MouseButtonEventHandler(cardclick);
                    Grid.SetColumn(backgroundimage, column);
                    Grid.SetRow(backgroundimage, row);
                    grid.Children.Add(backgroundimage);
                }
            }
        }

        private void cardclick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image) sender;
            ImageSource front = (ImageSource) card.Tag;
            ImageSource back = (ImageSource) card.DataContext;

            if (firstCard != card)
            {
                if (cardsOpen == 2)
                {
                    if (firstCard.Tag.ToString() != secondCard.Tag.ToString())
                    {
                        firstCard.Source = back;
                        secondCard.Source = back;
                    }
                    else
                    {
                        firstCard.Source = null;
                        secondCard.Source = null;
                    }

                    firstCard = null;
                    secondCard = null;
                    cardsOpen = 0;
                }

                if (firstCard == secondCard)
                {
                    firstCard = card;
                }
                else
                {
                    secondCard = card;
                }

                card.Source = front;
                cardsOpen++;

                if (CheckWinner())
                {
                    MessageBox.Show("You've won!'");
                }

                UpdateScore();

                UpdatePlayer();
            }
        }

        private void UpdatePlayer()
        {

        }

        private void UpdateScore()
        {

        }

        private bool CheckWinner()
        {
//            int imagesFlipped = 0;
//            images.ForEach(delegate (ImageSource img)
//            {
//                if (img == null)
//                {
//                    imagesFlipped++;
//                }
//            });
//
//            if (imagesFlipped == (cols * rows - 2))
//            {
//                return true;
//            }
//
            return false;
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
