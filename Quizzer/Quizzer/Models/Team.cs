using System.Collections.Generic;

namespace Quizzer.Models
{
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
            Points = 0;
        }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public int Points { get; set; }
    }
}