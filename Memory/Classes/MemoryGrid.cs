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

        private List<ImageSource> images = new List<ImageSource>();
        private List<Image> bgImages = new List<Image>();

        private GamePage gamePage;

        private Image firstCard;
        private Image secondCard;

        /// <summary>
        /// Initializes the given values of grid, rows, cols, gamepage, firstcard and secondcard.
        /// calls InitializeGameGrid(cols, rows);
        /// Puts new images or loads images in depending if loadGame is true.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <param name="backImages"></param>
        /// <param name="gamePage"></param>
        /// <param name="loadGame"></param>
        /// <param name="firstCard"></param>
        /// <param name="secondCard"></param>
        public MemoryGrid(Grid grid, int cols, int rows, List<Image> backImages, GamePage gamePage, bool loadGame, Image firstCard, Image secondCard)
        {
            this.rows = rows;
            this.cols = cols;
            this.grid = grid;
            this.gamePage = gamePage;
            this.firstCard = firstCard;
            this.secondCard = secondCard;
            InitializeGameGrid(cols, rows);
            if (loadGame)
            {
                LoadImages(backImages);
            }
            else
            {
                AddImages();
            }
        }

        /// <summary>
        /// Returns the grid.
        /// </summary>
        /// <returns>grid</returns>
        public Grid getGrid()
        {
            return grid;
        }

        /// <summary>
        /// Creates a list of all the images of the active theme, randomizes it and returns the list.
        /// </summary>
        /// <returns>images</returns>
        private List<ImageSource> GetImagesList()
        {
            for (int i = 0; i < (cols * rows); i++)
            {
                int imagenr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/" + imagenr + ".png", UriKind.Relative));
                images.Add(source);
            }

            images = randomize(images);

            return images;
        }

        /// <summary>
        /// Randomizes the given List<ImageSource> 
        /// </summary>
        /// <param name="imageSources"></param>
        /// <returns></returns>
        private List<ImageSource> randomize(List<ImageSource> imageSources)
        {
            Random random = new Random();
            for (int i = 0; i < ((cols * rows) / 2); i++)
            {
                int r = random.Next(0, (rows + cols));
                ImageSource temp = imageSources[r];
                imageSources[r] = imageSources[i];
                imageSources[i] = temp;
            }

            return imageSources;
        }

        /// <summary>
        /// Loads in the images from GetImagesList() and initializes all the images. 
        /// </summary>
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
                    backgroundimage.MouseDown += new MouseButtonEventHandler(gamePage.cardclick);

                    // adds the AnimationImage style to the cards for a pretty load in.
                    Style style = gamePage.FindResource("AnimationImage") as Style;
                    backgroundimage.Style = style;

                    Grid.SetColumn(backgroundimage, column);
                    Grid.SetRow(backgroundimage, row);
                    grid.Children.Add(backgroundimage);
                    bgImages.Add(backgroundimage);
                }
            }
        }

        /// <summary>
        /// Adds the "savedImages" cards to the grid.
        /// </summary>
        /// <param name="savedImages"></param>
        private void LoadImages(List<Image> savedImages)
        {
            int imageNumber = 0;
            // loops trough all the rows and cols
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundimage = savedImages[imageNumber];

                    // checks if any cards where opened when saved and shows the correct source.
                    if (backgroundimage.Tag.ToString() == firstCard.Source.ToString())
                    {
                        backgroundimage.Source = firstCard.Source;
                    }
                    else if(backgroundimage.Tag.ToString() == secondCard.Source.ToString())
                    {
                        backgroundimage.Source = secondCard.Source;
                    }
                    else
                    {
                        backgroundimage.Source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));

                    }
                    backgroundimage.MouseDown += new MouseButtonEventHandler(gamePage.cardclick);

                    backgroundimage.DataContext = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));

                    imageNumber++;

                    // adds the AnimationImage style to the cards for a pretty load in.
                    Style style = gamePage.FindResource("AnimationImage") as Style;
                    backgroundimage.Style = style;

                    Grid.SetColumn(backgroundimage, column);
                    Grid.SetRow(backgroundimage, row);
                    grid.Children.Add(backgroundimage);
                    bgImages.Add(backgroundimage);
                }
            }
        }

        /// <summary>
        /// Adds RowDefinitions and ColumnDefinitions to the grid. The amount depends on the given cols and rows.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
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

        /// <summary>
        /// Returns bgImages
        /// </summary>
        /// <returns>bgImages</returns>
        public List<Image> getBgImages()
        {
            return bgImages;
        }
    }
}
