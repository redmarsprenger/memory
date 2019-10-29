using System;
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
        private const int nr_cols = 4;
        private const int nr_rows = 4;
        MemoryGrid grid;
        private int cardsOpen;
        private string player1;
        private string player2;
        private string currentPlayer;

        private Image firstCard;
        private Image secondCard;
        private List<Image> bgImages = new List<Image>();
        private int player1Score;
        private int player2Score;

        private bool singleplayer { get; set; }
        private HighscoreList highscoreList = HighscoreList.Instance();

        public GamePage()
        {
            InitializeComponent();
        }

        public GamePage(string Player1)
        {
            InitializeComponent();
            this.player1 = Player1;

            this.singleplayer = true;
            txtBeurtNaam.Text = player1;

            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, Player1);
        }

        public GamePage(string Player1, string Player2)
        {
            InitializeComponent();
            this.player1 = Player1;
            this.player2 = Player2;

            this.singleplayer = false;
            txtBeurtNaam.Text = player1;
            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, Player1, Player2, this);

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
                    if (currentPlayer == player1)
                    {
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
                    }
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
                    if (CheckWinner())
                    {
                        FlipCards(card, front, back);
                        if (player2 != "")
                        {
                            MessageBox.Show(GameWinner());
                        }
                        else
                        {
                            MessageBox.Show("Klaar!'");
                        }
                    }
                }

                UpdateScore();

                currentPlayer = currentPlayer == player1 ? player2 : player1;

                UpdatePlayer(currentPlayer);
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

        private void UpdateScore(int score, string playername, string timer.text)
        {
            this.highscoreList.Load();

            //DateTime time = timer.text;

            //this.highscoreList.AddHighscore(new Highscore(playername, score, time));

            this.highscoreList.Save();
            
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





