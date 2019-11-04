using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Classes
{
    [Serializable]
    public class Savedgame
    {
        public int players { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public string playerbeurt {get; set;}
        public int score { get; set; }
        public TimeSpan time { get; set; }


    }
}
