﻿using System;
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
using Acc.Gui;

namespace Acc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DateTime date;
        public MainWindow()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            date = DateTime.Today;
            changeDate();
        }

        private void add_game(object sender, RoutedEventArgs e)
        {
        }

        private void add_players(object sender, RoutedEventArgs e)
        {
            var form = new UserWindow();
            form.ShowDialog();

        }

        private void game_list(object sender, RoutedEventArgs e)
        {
        }


        //изменение даты
        private void eventhandler(object sender, SelectionChangedEventArgs e)
        {
            date = datePicker1.SelectedDate.Value;
            changeDate();
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
            return d.ToString("MMMM dd");
        }
    }
}
