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
        public static Question CurrentQuestion { get; set; }

        public static async Task GetQuestions()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://opentdb.com/api.php?amount=50&difficulty=medium");
                Quiz = JsonConvert.DeserializeObject<Quiz>(response.Content.ReadAsStringAsync().Result);
                RandomiseAnswers();
            }
        }

        public static void RandomiseAnswers()
        {
            Quiz.results.ForEach(question =>
            {
                if (question.type != "boolean")
                {
                    var random = new Random();
                    var position = random.Next(0, question.incorrect_answers.Count);
                    var answers = question.incorrect_answers;
                    answers.Insert(position, question.correct_answer);
                    question.Answers = answers;
                }
            });
        }
    }
}