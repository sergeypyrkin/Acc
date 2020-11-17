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
        public CalendarPlayer(Player p)
        {
            player = p;
            Id = p.Id;
            Name = p.Name;
            FlagDel = p.FlagDel;
        }
    }
}
