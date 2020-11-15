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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {

        public List<Player> players;
        public UserWindow(List<Player> players)
        {
            InitializeComponent();
            this.players = players;
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

            Player p = new Player();
            p.Id = 0;
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
