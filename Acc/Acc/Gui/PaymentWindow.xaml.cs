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
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {

        public CalendarPlayer cplayer;
        public PaymentWindow(CalendarPlayer cplayer)
        {
            this.cplayer = cplayer;
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;

        }

        private void createPayment(object sender, RoutedEventArgs e)
        {
        }
    }
}
