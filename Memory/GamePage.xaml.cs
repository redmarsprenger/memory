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
using Memory.Classes;
using Memory.Properties;

namespace Memory
{
    public partial class GamePage : Page
    {
        private const int nr_cols = 4;
        private const int nr_rows = 4;
        MemoryGrid grid;


        public GamePage()
        {
            InitializeComponent();

            grid = new MemoryGrid(GameGrid, nr_cols, nr_rows);
        }
        public GamePage(SpelPage spelPage)
        {
            InitializeComponent();
            string savedGrid = File.ReadAllText("SavedGrid");
            grid = new MemoryGrid(savedGrid);
            Grid Grid = grid.getGrid();
            
        }

        public void pauzebtn_Click(object sender, RoutedEventArgs e)
        {
            /**        POGING TOT SPEL OPSLAAN          **/
            //            Grid Grid = grid.getGrid();
            //            string gamePageString = XamlWriter.Save(Grid);
            //
            //            File.WriteAllText("SavedGrid", gamePageString);
            /** Zorgt ervoor dat je terug gaat naar de huidige staat van het spel als je verder wilt gaan. **/
            var pausePage = new pausepage(this);
            NavigationService.Navigate(pausePage);

            //            NavigationService ns = NavigationService.GetNavigationService(this);
            //            ns.Navigate(new Uri("pausepage.xaml", UriKind.Relative));
        }
    }
    
}




