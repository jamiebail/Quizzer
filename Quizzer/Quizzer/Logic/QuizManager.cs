using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Quizzer.Models;

namespace Quizzer.Logic
{
    public static class QuizManager
    {
        public static Quiz Quiz { get; set; }
        public static List<Team> Teams { get; set; }

        public static async Task GetQuestions()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://opentdb.com/api.php?amount=50&difficulty=medium");
                Quiz = JsonConvert.DeserializeObject<Quiz>(response.Content.ReadAsStringAsync().Result);
            }
        } 
    }
}