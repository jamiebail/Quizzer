using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzer.Models
{
    public class Game
    {
        public List<Question> Questions { get; set; }
        public List<Team> Teams { get; set; }
        public DateTime PlayTime { get; set; }
    }
}