using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc.Entity
{
    public class GamePlayer
    {
        public Player player;
        public int N { get; set; }
        public int score1 { get; set; }
        public bool FlagDel { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public string sN
        {
            get
            {
                if (N == 0)
                {
                    return "";
                }
                return N.ToString();
            }
            set
            {
                
            }
        }

    }
}
