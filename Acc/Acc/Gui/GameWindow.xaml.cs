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
using System.Windows.Shapes;
using Acc.Entity;

namespace Acc.Gui
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        public List<Player> players;
        public List<GamePlayer> gplayers = new List<GamePlayer>();
        public GamePlayer ovPlayer;

        public GameWindow(List<Player> players)
        {
            InitializeComponent();
            this.players = players.Where(o => !o.FlagDel).ToList();
            datePicker1.SelectedDate = DateTime.Today;
            fillPlayers();
            updateGrid();

        }

        public void updateGrid()
        {
            datagrid.ItemsSource = gplayers;
            datagrid.Items.Refresh();
            colL.Content = gplayers.Sum(o => o.N);
        }

        private void fillPlayers()
        {
            int i = 1;
            foreach (Player p in players)
            {
                GamePlayer gp = new GamePlayer();
                gp.player = p;
                gp.Name = p.Name;
                gp.Number = i;
                i++;
                gplayers.Add(gp);
            }
        }

        private void create_game(object sender, RoutedEventArgs e)
        {
        }

        private void eventhandler(object sender, SelectionChangedEventArgs e)
        {
        }


        private void ut(object sender, MouseEventArgs e)
        {
            ListViewItem lv = sender as ListViewItem;
            ovPlayer = lv.Content as GamePlayer;
        }

        private void minusAction(object sender, RoutedEventArgs e)
        {
            if (ovPlayer.N == 0)
            {
                return;
            }
            ovPlayer.N = ovPlayer.N - 1;
            updateGrid();
        }

        private void plusAction(object sender, RoutedEventArgs e)
        {
            ovPlayer.N = ovPlayer.N + 1;
            updateGrid();
        }
    }
}
