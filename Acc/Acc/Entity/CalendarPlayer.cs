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


        public void setValues()
        {
            setValues(-17);

        }

        private void setValues(int v)
        {
            DateTime d1 = coreDay.AddDays(v);

            switch (v)
            {
                case -17:
                {
                    DayL17 = dateToString(d1);
                    break;
                }
            }
            return;
        }
    }
}
