using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc.Entity
{
    public class CalendarPlayer
    {

        public Player player;
        public int Id { get; set; }
        public string Name { get; set; }
        public bool FlagDel { get; set; }
        public int Number { get; set; }
        public DateTime coreDay;
        public CalendarPlayer(Player p, DateTime day)
        {
            player = p;
            Id = p.Id;
            Name = p.Name;
            FlagDel = p.FlagDel;
            coreDay = day;
        }

        private void changeDate()
        {

        }

        public string dateToString(DateTime d)
        {
            return d.ToString("MMMM dd");
        }



        public string DayL17 { get; set; }
        public string GameL17 { get; set; }
        public string PayL17 { get; set; }


        public string pl20 { get; set; }
        public string pl19 { get; set; }
        public string pl18 { get; set; }
        public string pl17 { get; set; }
        public string pl16 { get; set; }
        public string pl15 { get; set; }
        public string pl14 { get; set; }
        public string pl13 { get; set; }
        public string pl12 { get; set; }
        public string pl11 { get; set; }
        public string pl10 { get; set; }
        public string pl9 { get; set; }
        public string pl8 { get; set; }
        public string pl7 { get; set; }
        public string pl6 { get; set; }
        public string pl5 { get; set; }
        public string pl4 { get; set; }
        public string pl3 { get; set; }
        public string pl2 { get; set; }
        public string pl1 { get; set; }
        public string pl { get; set; }



        internal void setValues(DateTime date, Dictionary<string, Payment> pdict)
        {
            pl20 = getPayment(20, date, pdict);
            pl19 = getPayment(19, date, pdict);
            pl18 = getPayment(18, date, pdict);
            pl17 = getPayment(17, date, pdict);
            pl16 = getPayment(16, date, pdict);
            pl15 = getPayment(15, date, pdict);
            pl14 = getPayment(14, date, pdict);
            pl13 = getPayment(13, date, pdict);
            pl12 = getPayment(12, date, pdict);
            pl11 = getPayment(11, date, pdict);
            pl10 = getPayment(10, date, pdict);
            pl9 = getPayment(9, date, pdict);
            pl8 = getPayment(8, date, pdict);
            pl7 = getPayment(7, date, pdict);
            pl6 = getPayment(6, date, pdict);
            pl5 = getPayment(5, date, pdict);
            pl4 = getPayment(4, date, pdict);
            pl3 = getPayment(3, date, pdict);
            pl2 = getPayment(2, date, pdict);
            pl1 = getPayment(1, date, pdict);
            pl = getPayment(0, date, pdict);

        }

        private string getPayment(int i, DateTime date, Dictionary<string, Payment> pdict)
        {
            DateTime currDateTime = date.AddDays(-i);
            string t = String.Format("{0}_{1}", player.Id, currDateTime.ToString());
            if (!pdict.ContainsKey(t))
            {
                return "";
            }
            Payment payment = pdict[t];
            return "+" + payment.price;

        }
    }
}
