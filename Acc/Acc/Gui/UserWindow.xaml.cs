using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {

        public List<Player> players;
        private Player player;
        private string mode = "new";

        public UserWindow(List<Player> players)
        {
            InitializeComponent();
            this.players = players;
            mode = "new";
        }

        public UserWindow(List<Player> players, Player player)
        {
            InitializeComponent();
            this.players = players;
            this.player = player;
            mode = "edit";
            fio.Text = player.Name;
            description.Text = player.Description;
            price.Text = player.StartBalance.ToString();

        }


        //создание пользователя
        private void createUser(object sender, RoutedEventArgs e)
        {
            string validText = isValidUser();
            if (!String.IsNullOrEmpty(validText))
            {
                MessageBox.Show(validText);
                return;
            }

            if (mode == "new")
            {
                createPlayer();
            }
            if (mode == "edit")
            {
                editPlayer();
            }

            DialogResult = true;
        }

        public void editPlayer()
        {
            player.Name = fio.Text;
            player.Description = description.Text;
            player.StartBalance = Convert.ToInt32(price.Text);
        }

        public void createPlayer()
        {
            Player p = new Player();
            int id = 0;
            if (players.Count > 0)
            {
                List<int> ids = new List<int>();
                foreach (var player in players)
                {
                    ids.Add(player.Id);
                }
                id = ids.Max() + 1;
            }

            p.Id = id;
            p.Name = fio.Text;
            p.Description = description.Text;
            p.StartBalance = Convert.ToInt32(price.Text);
            players.Add(p);

            Player.SaveToFile("players", players);
        }

        public string isValidUser()
        {
            string result = "";
            if (fio.Text == "")
            {
                result = "Поле фио не должно быть пустое";
                return result;
            }

            if (fio.Text != "")
            {
                Player p = null;
                if (mode == "new")
                {
                    p = players.FirstOrDefault(o => o.Name == fio.Text && !o.FlagDel);
                }

                if (mode == "edit")
                {

                    p = players.FirstOrDefault(o => o.Name == fio.Text && !o.FlagDel && o.Id != player.Id);
                }

                if (p != null)
                {
                    result = "Уже есть человек с таким именем";
                    return result;
                }
            }

            if (price.Text == "")
            {
                result = "Поле price не должно быть пустое";
                int value;

                bool success = Int32.TryParse(price.Text, out value);
                if (!success)
                {
                    result = "Поле price должно быть целым значением";
                    return result;
                }

            }

            if (price.Text != "")
            {
                int value;

                bool success = Int32.TryParse(price.Text, out value);
                if (!success)
                {
                    result = "Поле price должно быть целым значением";
                    return result;
                }

            }
            return "";
        }
    }
}
