using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
        private MemoryGrid grid;
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

        private HighscoreList highscoreList = HighscoreList.Instance();

        private DispatcherTimer timer = new DispatcherTimer();
        private bool timerInstance;
        public int TotalTime;

        private int singlePlayerScore;

        public bool SaveGamePage()
        {
            var GridToSave = grid.getGrid();


            TextWriter tw = new StreamWriter("GamePage.txt");

            // write lines of text to the file
            tw.WriteLine(cardsOpen);
            tw.WriteLine(player1);
            tw.WriteLine(player2);
            tw.WriteLine(currentPlayer);
            if (firstCard == null)
            {
                firstCard = new Image();
                tw.WriteLine();
            }
            else
            {
                tw.WriteLine(firstCard.Tag.ToString());
            }
            if (secondCard == null)
            {
                secondCard = new Image();
                tw.WriteLine();
            }
            else
            {
                tw.WriteLine(secondCard.Tag.ToString());
            }
            
            tw.WriteLine(player1Score);
            tw.WriteLine(player2Score);
            tw.WriteLine(singlePlayer);
            tw.WriteLine(timerInstance);
            tw.WriteLine(TotalTime);
            tw.WriteLine(singlePlayerScore);
            //            tw.WriteLine(grid);
            //            tw.WriteLine(bgImages);
            //            tw.WriteLine(highscoreList);
            //            tw.WriteLine(timer);

            // close the stream     
            tw.Close();


            TextWriter twImages = new StreamWriter("bgImages.txt");

            foreach (var bg in bgImages)
            {
                twImages.WriteLine(bg.Tag);
            }

            twImages.Close();

            return false;
        }

        public void loadGame()
        {
            firstCard = new Image();
            secondCard = new Image();

            TextReader tr = new StreamReader("GamePage.txt");

            cardsOpen = Convert.ToInt32(tr.ReadLine());
            player1 = tr.ReadLine();
            player2 = tr.ReadLine();
            currentPlayer = tr.ReadLine();

            var card1 = (string)tr.ReadLine();
            var card2 = (string)tr.ReadLine();
            firstCard.Tag = stringToBitMap(card1);
            secondCard.Tag = stringToBitMap(card2);
            firstCard.Source = stringToBitMap(card1);
            secondCard.Source = stringToBitMap(card2);
            firstCard.DataContext = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));
            secondCard.DataContext = new BitmapImage(new Uri("Resources/themes/" + (string)Settings.Default["ThemeName"] + "/achterkant.png", UriKind.Relative));

            player1Score = Convert.ToInt32(tr.ReadLine());
            player2Score = Convert.ToInt32(tr.ReadLine());
            singlePlayer = Convert.ToBoolean(tr.ReadLine());
            timerInstance = Convert.ToBoolean(tr.ReadLine());
            TotalTime = Convert.ToInt32(tr.ReadLine());
            singlePlayerScore = Convert.ToInt32(tr.ReadLine());
            
            // close the stream
            tr.Close();
            TextReader trImages = new StreamReader("bgImages.txt");

            using (trImages)
            {
                string line;
                for (int i = 0; i < (nr_cols * nr_rows); i++)
                {
                    Image readImage = new Image();
                    readImage.Tag = trImages.ReadLine();
                    readImage.Name = "x" + i;
                    bgImages.Add(readImage);
                }
            }


            trImages.Close();
        }

        public GamePage(bool loadGame)
        {
            InitializeComponent();
            if (loadGame)
            {
                this.loadGame();
                txtBeurtNaam.Text = currentPlayer;
                grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, bgImages, this, true, firstCard, secondCard);
                bgImages = grid.getBgImages();
                SetCards();


//                ImageSource firstCardFront = (ImageSource)firstCard.Tag;
//                ImageSource firstCardBack = (ImageSource)firstCard.DataContext;
//                firstCard.Source = firstCardBack;
////                FlipCards(firstCard, firstCardFront, firstCardBack);
//
//                ImageSource secondCardFront = (ImageSource)secondCard.Tag;
//                ImageSource secondCardBack = (ImageSource)secondCard.DataContext;
//                secondCard.Source = secondCardBack;
//                FlipCards(secondCard, secondCardFront, secondCardBack);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public GamePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The instance Page_load created and on page_loaded the if-else statement is created where it will ask if the timer is already created or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Page_loaded(object sender, RoutedEventArgs e)
        {
            // if the starting value is false initializes the timer
            if (timerInstance == false)
            {
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += TimeTicker;
                timer.Start();
                timerInstance = true;
            }
            else // resume the timer
            {
                timer.Start();
            }
        }

        /// <summary>
        /// creates the indicator of how much the timer is on
        /// TimeTickers creates the development of how the value of Totaltime is increased
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimeTicker(object sender, EventArgs e) 
        {
            TotalTime++;
            Timerlabel.Content = TotalTime.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Player1"></param>
        public GamePage(string Player1)
        {
            InitializeComponent();
            this.player1 = Player1;

            singlePlayer = true;

            txtBeurtNaam.Text = player1;
            currentPlayer = player1;

            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, bgImages, this, false, firstCard, secondCard);

            bgImages = grid.getBgImages();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Player1"></param>
        /// <param name="Player2"></param>
        public GamePage(string Player1, string Player2)
        {
            InitializeComponent();
            this.player1 = Player1;
            this.player2 = Player2;

            singlePlayer = false;

            txtBeurtNaam.Text = player1;
            currentPlayer = player1;
            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows, bgImages, this, false, firstCard, secondCard);
            bgImages = grid.getBgImages();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spelPage"></param>
        /// <param name="Player1"></param>
        public GamePage(SpelPage spelPage, string Player1)
        {
            InitializeComponent();
            string savedGrid = File.ReadAllText("SavedGrid");
            grid = new MemoryGrid(savedGrid, Player1);
            Grid Grid = grid.getGrid();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void pauzebtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pausepage(this));
            // puts a stop on the timer. //
            timer.Stop();
        }

        
        /// <summary>
        /// Updates the current player textbox with given string
        /// </summary>
        /// <param name="newPlayer"></param>
        public void UpdatePlayer(string newPlayer)
        {
            txtBeurtNaam.Text = newPlayer;
        }

        private BitmapImage stringToBitMap(string stringPath)
        {
            Uri imageUri = new Uri("about:blank");
            try
            {
                imageUri = new Uri(stringPath, UriKind.Relative);
            }
            catch
            {
                imageUri = new Uri(stringPath);
            }
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            return imageBitmap;
        }


        SoundPlayer FlipSound = new SoundPlayer(@"../../Resources/music/cardflip.wav");
        SoundPlayer WinSound = new SoundPlayer(@"../../Resources/music/win.wav");
        SoundPlayer FailSound = new SoundPlayer(@"../../Resources/music/fail.wav");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cardclick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;

            card.Tag = stringToBitMap(card.Tag.ToString());

            ImageSource front = (ImageSource)card.Tag;
            ImageSource back = (ImageSource)card.DataContext;

            if (firstCard != card && secondCard != card)
            {
                if (firstCard == null || secondCard == null)
                {
                    runCardClickEvents(card, front, back);
                }
                else if (firstCard.Source != card.Source && secondCard.Source != card.Source)
                {
                    runCardClickEvents(card, front, back);
                }
            }
        }

        private void runCardClickEvents(Image card, ImageSource front, ImageSource back)
        {
            if ((bool)Settings.Default["Sound"])
            {
                WinSound.Stop();
                FailSound.Stop();
                FlipSound.Play();
            }
            
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
            if (cardsOpen == 2 && !singlePlayer && firstCard.Tag.ToString() != secondCard.Tag.ToString())
            {
                if ((bool)Settings.Default["Sound"])
                {
                    FlipSound.Stop();
                    FailSound.Play();
                }
                currentPlayer = (currentPlayer == player1) ? player2 : player1;
                UpdatePlayer(currentPlayer);
            }

            //soundeffect if you have an equal set of cards
            else if (cardsOpen == 2 && firstCard.Source.ToString() == secondCard.Tag.ToString())
            {
                if ((bool)Settings.Default["Sound"])
                {
                    FlipSound.Stop();
                    WinSound.Play();
                }
            }

            //soundeffect if you have an unequal set of cards
            else if (cardsOpen == 2 && firstCard.Source.ToString() != secondCard.Tag.ToString())
            {
                if ((bool) Settings.Default["Sound"])
                {
                    FlipSound.Stop();
                    FailSound.Play();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="front"></param>
        /// <param name="back"></param>
        private void FlipCards(Image card, ImageSource front, ImageSource back)
        {
            if (firstCard.Tag.ToString() != secondCard.Tag.ToString())
            {
                foreach (var img in bgImages)
                {
                    if (img.Tag != null && img.Tag != "")
                    {
                        img.Source = back;
                    }
                }
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

        private void SetCards()
        {
            foreach (Image img in bgImages)
            {
                if (img.Tag == "")
                {
                    img.Source = null;
                    img.MouseDown -= new MouseButtonEventHandler(this.cardclick);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

                return "U heeft het spel voltooid met een score van: " + singlePlayerScore;
            }
        }
        
        /// <summary>
        /// used to submit the highscore of the player
        /// </summary>
        /// <param name="playername">string of the playername</param>
        /// <param name="score">int of the score</param>
        /// <param name="timer">int of the time in seconds</param>
        private void SubmitScore(string playername, int score, int timer)
        {
            int minutes = timer / 60;
            int seconds = timer % 60;

            string time = minutes + " : " + seconds;

            singlePlayerScore += score * 4 / (1 + minutes);

            highscoreList.AddHighscore(new Highscore(playername, singlePlayerScore, time));
            highscoreList.Save();
        }

        /// <summary>
        /// updates the score of the current player if a card is flipped
        /// </summary>
        private void UpdateScore()
        {            
            if (!singlePlayer && cardsOpen == 2 && firstCard.Tag.ToString() == secondCard.Tag.ToString())
            {
                if (currentPlayer == player1)
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
            else if(singlePlayer)
            {
                this.player1Score++;
                txtScore_1.Text = player1Score.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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





