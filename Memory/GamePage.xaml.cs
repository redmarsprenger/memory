﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Memory.Classes;
using Memory.Properties;

namespace Memory
{
    public partial class GamePage : Page
    {
        MemoryGrid grid;
        private const int nr_cols = 4;
        private const int nr_rows = 4;
        private int cardsOpen;
        private string player1;
        private string player2 = "";
        private string currentPlayer;

        private Image firstCard;
        private Image secondCard;
        private List<Image> bgImages = new List<Image>();
        private int player1Score;
        private int player2Score;
        private bool singlePlayer;


        private bool singleplayer { get; set; }
        private HighscoreList highscoreList = HighscoreList.Instance();

        public GamePage()
        {
            InitializeComponent();
        }
        // start of the timer code: //

        // announces that the timer is created and what its name is. //
        DispatcherTimer timer = new DispatcherTimer();
        // a bool is created so that the timer wont stack. //
        private bool timerInstance = false;

        // here is the instance Page_load created and on page_loaded the if-else statement is created where it will ask if the timer is already created or not. //
        public void Page_loaded(object sender, RoutedEventArgs e)
        {
            // checks if the starting value is false, and if it is it will indicate how the timer shall be handled.
            if (timerInstance == false)
            {
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += TimeTicker;
                timer.Start();
                timerInstance = true;
            }
            // if the timerinstance is already true it will simply resume the timer.//
            else
            {
                timer.Start();
            }
        }
        // creates the indicator of how much the timer is on. //
        public int TotalTime = 0;
        // TimeTickers creates the development of how the value of Totaltime is increased. //
        public void TimeTicker(object sender, EventArgs e) 
        {
            TotalTime++;
            Timerlabel.Content = TotalTime.ToString();
        }
// end of the timer code // 
        public GamePage(string Player1)
        {
            InitializeComponent();
            this.player1 = Player1;

            singlePlayer = true;
            txtBeurtNaam.Text = player1;
            currentPlayer = player1;
            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, Player1, this);
            bgImages = grid.getBgImages();
        }

        public GamePage(string Player1, string Player2)
        {
            InitializeComponent();
            this.player1 = Player1;
            this.player2 = Player2;

            singlePlayer = false;

            txtBeurtNaam.Text = player1;
            currentPlayer = player1;
            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, Player1, Player2, this);
            bgImages = grid.getBgImages();
        }

        public GamePage(SpelPage spelPage, string Player1)
        {
            InitializeComponent();
            string savedGrid = File.ReadAllText("SavedGrid");
            grid = new MemoryGrid(savedGrid, Player1);
            Grid Grid = grid.getGrid();

        }

        public void pauzebtn_Click(object sender, RoutedEventArgs e)
        {
            var pausePage = new pausepage(this);
            NavigationService.Navigate(pausePage);
            // puts a stop on the timer. //
            timer.Stop();

            
        }

        public void UpdatePlayer(string newPlayer)
        {
            txtBeurtNaam.Text = newPlayer;
        }

        public void cardclick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            ImageSource back = (ImageSource)card.DataContext;

            if (firstCard != card)
            {
                if (cardsOpen == 2)
                {
                    FlipCards(card, front, back);
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
                if (cardsOpen == 2)
                {
                    if (!singlePlayer)
                    {
                        currentPlayer = (currentPlayer == player1) ? player2 : player1;

                        UpdatePlayer(currentPlayer);
                    }
                }
                UpdateScore();

                if (cardsOpen == 2 && CheckWinner())
                {
                    FlipCards(card, front, back);
                    MessageBox.Show(GameWinner());
                    NavigationService.Navigate(new WelkomPage());
                }
            }
        }

        private void FlipCards(Image card, ImageSource front, ImageSource back)
        {
            if (firstCard.Tag.ToString() != secondCard.Tag.ToString())
            {
                firstCard.Source = back;
                secondCard.Source = back;
            }
            else
            {
                foreach (Image img in bgImages)
                {
                    if (img.Tag != null)
                    {
                        if (firstCard.Source.ToString() == img.Tag.ToString())
                        {
                            img.Tag = null;
                        }
                    }
                }
                firstCard.Source = null;
                secondCard.Source = null;
            }

            firstCard = null;
            secondCard = null;
        }

        private string GameWinner()
        {
            if (!singlePlayer)
            {
                string winner;
                if (player1Score > player2Score)
                {
                    winner = player1;
                }
                else if (player1Score < player2Score)
                {
                    winner = player2;
                }
                else
                {
                    return "Gelijkspel!";
                }
                return winner + " heeft gewonnen!";
            }
            else
            {
                SubmitScore(this.player1, this.player1Score, this.TotalTime);

                return "U heeft het spel voltooid met een score van: " + this.player1Score;
            }
        }

        private void SubmitScore(string playername, int score, int timer)
        {
            int minutes = timer / 60;
            int seconds = timer % 60;

            string time = minutes + " : " + seconds;

            highscoreList.AddHighscore(new Highscore(playername, score, time));
            highscoreList.Save();
        }

        private void UpdateScore()
        {            
            if (!singlePlayer && cardsOpen == 2 && firstCard.Tag.ToString() == secondCard.Tag.ToString())
            {
                if (currentPlayer != player1)
                {
                    player1Score++;
                }
                else
                {
                    player2Score++;
                }
                txtScore_1.Text = player1Score.ToString();
                txtScore_2.Text = player2Score.ToString();
            }
            else
            {
                this.player1Score++;
                txtScore_1.Text = player1Score.ToString();
            }
        }

        private bool CheckWinner()
        {
            int imagesFlipped = 0;
            int imagesFlipped2 = 0;
            bgImages.ForEach(delegate (Image img)
            {
                imagesFlipped2++;
                if (img.Tag == null)
                {
                    imagesFlipped++;
                }
            });

            if (imagesFlipped == (nr_cols * nr_rows) - 2)
            {
                return true;
            }

            return false;
        }

    }
}





