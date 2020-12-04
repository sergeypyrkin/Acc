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
        public Payment p;
        public List<Payment> payments;
        public string mode = "";
        public PaymentWindow(CalendarPlayer cplayer, List<Payment> payments)
        {
            this.cplayer = cplayer;
            this.payments = payments;
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            nameUser.Content = cplayer.Name;
            mode = "new";
            b1.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Hidden;
            b3.Visibility = Visibility.Hidden;

        }


        public PaymentWindow(CalendarPlayer cplayer, Payment p,  List<Payment> payments)
        {
            this.cplayer = cplayer;
            this.payments = payments;
            this.p = p;
            InitializeComponent();
            datePicker1.SelectedDate = p.date;
            datePicker1.IsEnabled = false;
            price.Text = p.price.ToString();
            nameUser.Content = cplayer.Name;
            mode = "edit";
            b1.Visibility = Visibility.Hidden;
            b2.Visibility = Visibility.Visible;
            b3.Visibility = Visibility.Visible;
        }



        private void createPayment(object sender, RoutedEventArgs e)
        {
            string validText = isValidPayment();
            if (!String.IsNullOrEmpty(validText))
            {
                MessageBox.Show(validText);
                return;
            }
            createPayment();
            DialogResult = true;


        }

        public void createPayment()
        {
            Payment payment = new Payment();
            int id = 0;
            if (payments.Count > 0)
            {
                List<int> ids = new List<int>();
                foreach (var p in payments)
                {
                    ids.Add(p.Id);
                }
                id = ids.Max() + 1;
            }

            payment.Id = id;
            payment.playerId = cplayer.Id;
            payment.date = (DateTime) datePicker1.SelectedDate;
            payment.price = Convert.ToInt32(price.Text);
            payments.Add(payment);

            Payment.SaveToFile(payments);
        }

        private string isValidPayment()
        {
            string result = "";
            if (price.Text == "")
            {
                result = "Поле платеж не должно быть пустое";
                int value;

                bool success = Int32.TryParse(price.Text, out value);
                if (!success)
                {
                    result = "Поле платеж должно быть целым значением";
                    return result;
                }

            }

            if (price.Text != "")
            {
                int value;

                bool success = Int32.TryParse(price.Text, out value);
                if (!success)
                {
                    result = "Поле платеж должно быть целым значением";
                    return result;
                }

            }

            if (price.Text == "0")
            {
                 
                    result = "Платеж не может быть нулевым";
                    return result;
                
            }


            var dt = datePicker1.SelectedDate;
            if (payments.Exists(o => o.date == dt && o.playerId == cplayer.Id))
            {
                Payment p = payments.FirstOrDefault(o => o.date == dt && o.playerId == cplayer.Id);
                result = "У этого человека в этот день уже есть платеж";
                return result;
            }
            return "";
        }

        private void removePayment(object sender, RoutedEventArgs e)
        {
        }

        private void savePayment(object sender, RoutedEventArgs e)
        {
        }
    }
}
