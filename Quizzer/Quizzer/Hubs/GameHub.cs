using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using Microsoft.AspNet.SignalR;
using Quizzer.Logic;
using Quizzer.Models;
using WebGrease.Css.Extensions;

namespace Quizzer.Hubs
{
    public class GameHub : Hub
    {
        public int Answered { get; set; }
        public Question CurrentQuestion { get; set; }
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
        public void AddTeam(string name)
        {
            if (name != null)
            {
                if (QuizManager.Teams == null)
                    QuizManager.Teams = new List<Team>();

                QuizManager.Teams.Add(new Team()
                {
                    Name = name,
                    Points = 0
                });

                Clients.All.newTeam(name);
                Groups.Add(Context.ConnectionId, name);
            }
        }

        public void Answer(int team, string answer)
        {
            CurrentQuestion.Answered++;
            if (CurrentQuestion.Answered == QuizManager.Teams.Count)
            {
                NextQuestion(QuizManager.Quiz.results[QuizManager.Quiz.results.IndexOf(CurrentQuestion) + 1]);
            }
        }

        public void NextQuestion(Question question)
        {
            CurrentQuestion = question;
            Clients.All.nextQuestion(question);
        }

        public async Task StartGame()
        {
            NextQuestion(QuizManager.Quiz.results.First());
        }

        public void ClearTeams()
        {
            QuizManager.Teams = new List<Team>();
            Clients.All.resetTeams();
        }

        public void JoinTeam(string team, string player)
        {
            Team t = QuizManager.Teams.FirstOrDefault(x => x.Name == team);
            t.Players.Add(new Player
            {
                Name = player
            });

            Groups.Add(Context.ConnectionId, team);
            var players = t.Players;
            Thread.Sleep(300);
            Clients.Group(team).addPlayerToTeam(players);
        }
       
    }
}