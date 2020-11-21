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
using Acc.Entity;
using Acc.Gui;

namespace Acc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Player> players;
        public List<CalendarPlayer> cplayers = new List<CalendarPlayer>();
        public DateTime date;
        private ContextMenu contextMenuDataGrid = new ContextMenu();
        public MainWindow()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            date = DateTime.Today;
            changeDate();
            players = Player.LoadFromFile("players");
            ReloadDataGrid();
            datagrid.ContextMenu = contextMenuDataGrid;

        }



        public void ReloadDataGrid()
        {
            if (players == null)
            {
                return;
            }
            cplayers.Clear();
            foreach (var player in players)
            {
                cplayers.Add(new CalendarPlayer(player, date));

            }
            cplayers = cplayers.Where(o => !o.FlagDel).OrderBy(o => o.Name).ToList();
            int number = 1;
            foreach (var cpl in cplayers)
            {
                cpl.Number = number;
                number = number + 1;
            }

            foreach (var cpl in cplayers)
            {
                cpl.setValues();
            }

            hL17.Content = dateToString(date.AddDays(-17));

            datagrid.ItemsSource = cplayers;
            datagrid.Items.Refresh();
            
        }

        private void add_game(object sender, RoutedEventArgs e)
        {
        }

        private void add_players(object sender, RoutedEventArgs e)
        {
            var form = new UserWindow(players);
            form.ShowDialog();
            ReloadDataGrid();

        }

        private void game_list(object sender, RoutedEventArgs e)
        {
        }


        //изменение даты
        private void eventhandler(object sender, SelectionChangedEventArgs e)
        {
            date = datePicker1.SelectedDate.Value;
            changeDate();
            ReloadDataGrid();
        }

       
        private void changeDate()
        {
            DateTime d1 = date.AddDays(-27);
            DateTime d2 = date.AddDays(+3);
            this.ot.Content = dateToString(d1);
            this.after.Content = dateToString(d2);

        }

        public string dateToString(DateTime d)
        {
            return d.ToString("dd MMMM");
        }


        //selection changed
        private void dataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contextMenuDataGrid.Items.Clear();

            if (datagrid.SelectedItems.Count == 1)
            {
                CalendarPlayer cp = (CalendarPlayer)datagrid.SelectedItems[0];
                {
                    MenuItem mi11 = new MenuItem();
                    mi11.Header = "ДОБАВИТЬ ПЛАТЕЖ";
                    mi11.Click += mi_add_pay;
                    mi11.Tag = cp;
                    contextMenuDataGrid.Items.Add(mi11);
                }


                {
                    MenuItem mi11 = new MenuItem();
                    mi11.Header = "РЕДАКТИРОВАТЬ";
                    mi11.Click += mi_edit_player;
                    mi11.Tag = cp;
                    contextMenuDataGrid.Items.Add(mi11);
                }

                {
                    MenuItem mi11 = new MenuItem();
                    mi11.Header = "УДАЛИТЬ ИГРОКА";
                    mi11.Click += mi_delete_player;
                    mi11.Tag = cp;
                    contextMenuDataGrid.Items.Add(mi11);
                }

            }
        }

        private void mi_delete_player(object sender, RoutedEventArgs e)
        {
            CalendarPlayer mk = (sender as MenuItem).Tag as CalendarPlayer;
            if (MessageBox.Show(String.Format("Вы действительно хотите удалить {0}", mk.player.Name), "Подтверждение"
                  , MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;
            mk.player.FlagDel = true;
            Player.SaveToFile("players", players);
            ReloadDataGrid();
        }


        //редактирование игрока
        private void mi_edit_player(object sender, RoutedEventArgs e)
        {

            CalendarPlayer mk = (sender as MenuItem).Tag as CalendarPlayer;
            var form = new UserWindow(players, mk.player);
            form.ShowDialog();
            Player.SaveToFile("players", players);
            ReloadDataGrid();
        }


        //добавить платеж
        private void mi_add_pay(object sender, RoutedEventArgs e)
        {


        }
    }
}
