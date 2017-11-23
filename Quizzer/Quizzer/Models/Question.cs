﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzer.Models
{
    public class Question
    {
        public string category { get; set; }
        public string type { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public List<string> incorrect_answers { get; set; }
        public int Answered { get; set; }
    }
}