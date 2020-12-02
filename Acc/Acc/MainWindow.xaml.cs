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
        public List<Payment> payments;

        public List<CalendarPlayer> cplayers = new List<CalendarPlayer>();
        public DateTime date;
        private ContextMenu contextMenuDataGrid = new ContextMenu();
        public MainWindow()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            date = DateTime.Today;
            changeDate();
            players = Player.LoadFromFile();
            payments = Payment.LoadFromFile();
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

            Dictionary<string, Payment> pdict = new Dictionary<string, Payment>();
            foreach (Payment p in payments)
            {
                string t = String.Format("{0}_{1}", p.playerId, p.date.ToString());
                pdict.Add(t,p);
            }

            hL20.Content = dateToString(date.AddDays(-20));
            hL19.Content = dateToString(date.AddDays(-19));
            hL18.Content = dateToString(date.AddDays(-18));
            hL17.Content = dateToString(date.AddDays(-17));
            hL16.Content = dateToString(date.AddDays(-16));
            hL15.Content = dateToString(date.AddDays(-15));
            hL14.Content = dateToString(date.AddDays(-14));
            hL13.Content = dateToString(date.AddDays(-13));
            hL12.Content = dateToString(date.AddDays(-12));
            hL11.Content = dateToString(date.AddDays(-11));
            hL10.Content = dateToString(date.AddDays(-10));
            hL9.Content = dateToString(date.AddDays(-9));
            hL8.Content = dateToString(date.AddDays(-8));
            hL7.Content = dateToString(date.AddDays(-7));
            hL6.Content = dateToString(date.AddDays(-6));
            hL5.Content = dateToString(date.AddDays(-5));
            hL4.Content = dateToString(date.AddDays(-4));
            hL3.Content = dateToString(date.AddDays(-3));
            hL2.Content = dateToString(date.AddDays(-2));
            hL1.Content = dateToString(date.AddDays(-1));
            hL.Content = dateToString(date.AddDays(0));
            

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
            Player.SaveToFile(players);
            ReloadDataGrid();
        }


        //редактирование игрока
        private void mi_edit_player(object sender, RoutedEventArgs e)
        {

            CalendarPlayer mk = (sender as MenuItem).Tag as CalendarPlayer;
            var form = new UserWindow(players, mk.player);
            form.ShowDialog();
            Player.SaveToFile(players);
            ReloadDataGrid();
        }


        //добавить платеж
        private void mi_add_pay(object sender, RoutedEventArgs e)
        {

            CalendarPlayer mk = (sender as MenuItem).Tag as CalendarPlayer;
            addPayment(mk);

        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void UIChangeGame(object sender, MouseButtonEventArgs e)
        {
        }

        private void UIChangePrice(object sender, MouseButtonEventArgs e)
        {
        }


        private void Datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagrid.SelectedItems.Count == 0)
            {
                return;
            }
            var item = datagrid.SelectedItems[0];
            CalendarPlayer mk = item as CalendarPlayer;
            addPayment(mk);
        }

        private void  addPayment(CalendarPlayer mk)
        {
            var form = new PaymentWindow(mk, payments);
            form.ShowDialog();
            //Player.SaveToFile("players", players);
            ReloadDataGrid();
        }
    }
}
