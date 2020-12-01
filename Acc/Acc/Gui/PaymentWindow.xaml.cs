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
        public List<Payment> payments;
        public PaymentWindow(CalendarPlayer cplayer, List<Payment> payments)
        {
            this.cplayer = cplayer;
            this.payments = payments;
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            nameUser.Content = cplayer.Name;

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
            payment.date = datePicker1.DisplayDate;
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


            DateTime dt = datePicker1.DisplayDate;
            if (payments.Exists(o => o.date == dt && o.playerId == cplayer.Id))
            {
                result = "У этого человека в этот день уже есть платеж";
                return result;
            }
            return "";
        }
    }
}
