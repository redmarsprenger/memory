﻿using System;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <param name="player"></param>
        /// <param name="gamePage"></param>
        public MemoryGrid(Grid grid, int cols, int rows, List<Image> backImages, GamePage gamePage, bool loadGame)
        {
            this.rows = rows;
            this.cols = cols;
            this.grid = grid;
            this.gamePage = gamePage;
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

//        // Two player grid
//        public MemoryGrid(Grid grid, int cols, int rows, List<Image> backImages, GamePage gamePage, bool loadGame)
//        {
//            this.rows = rows;
//            this.cols = cols;
//            this.grid = grid;
//            this.gamePage = gamePage;
//            InitializeGameGrid(cols, rows);
//            if (loadGame)
//            {
//                LoadImages(backImages);
//            }
//            else
//            {
//                AddImages();
//            }
//        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedGrid"></param>
        /// <param name="player"></param>
        public MemoryGrid(String savedGrid, string player)
        {
            StringReader stringReader = new StringReader(savedGrid);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid readerLoadGrid = (Grid)XamlReader.Load(xmlReader);
            this.grid = readerLoadGrid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Grid getGrid()
        {
            return grid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
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
        /// 
        /// </summary>
        private void LoadImages(List<Image> savedImages)
        {
            images = GetImagesList();
            int imageNumber = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundimage = savedImages[imageNumber];

                    backgroundimage.Source = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));
//                    backgroundimage.Tag = images[imageNumber];
                    imageNumber++;
                    backgroundimage.DataContext = backgroundimage.Source;
                    backgroundimage.MouseDown += new MouseButtonEventHandler(gamePage.cardclick);

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
        /// 
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Image> getBgImages()
        {
            return bgImages;
        }
    }
}
