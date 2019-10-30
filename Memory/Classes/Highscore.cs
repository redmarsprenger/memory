using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Classes
{
    /// <summary>
    /// Highscore object used to store the data of a player after they finish a game
    /// </summary>
    [Serializable]
    public class Highscore
    {
        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Score the player got during the game
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Time of the duration of the game
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">string of the player name</param>
        /// <param name="Score">int of the score achieved in the game</param>
        /// <param name="Time">string of the time it took to finish a game</param>
        public Highscore(string Name, int Score, string Time)
        {
            this.Name = Name;
            this.Score = Score;
            this.Time = Time;
        }
    }
}
