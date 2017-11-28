using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Quizzer.Logic;
using Quizzer.Models;

namespace Quizzer.Hubs
{
    public class GameHub : Hub
    {
        public int Answered { get; set; }

        public override Task OnDisconnected(bool stopCalled)
        {
            base.OnDisconnected(stopCalled);
            if (QuizManager.Teams != null)
            {
                QuizManager.Teams.ForEach(team =>
                {
                    var user = team.Players.FirstOrDefault(player => player.ConnectionId == Context.ConnectionId);
                    if (user != null)
                    {

                        team.Players.Remove(user);
                    }
                    UpdateTeams(team);
                });
            }
            return null;
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AddTeam(string name, string creator)
        {
            if (name != null)
            {
                if (QuizManager.Teams == null)
                    QuizManager.Teams = new List<Team>();

                var team = new Team()
                {
                    Name = name,
                    Points = 0,
                    Players = new List<Player>
                    {
                        new Player
                        {
                            Name = creator,
                            ConnectionId = Context.ConnectionId
                        }
                    }
                };

                QuizManager.Teams.Add(team);

                Clients.All.newTeam(name);
                Groups.Add(Context.ConnectionId, name);
                Clients.Group(name).addPlayerToTeam(team.Players);
            }
        }

        public void Answer(string team, string answer)
        {
            QuizManager.CurrentQuestion.Answered++;
            if (QuizManager.CurrentQuestion.Answered == QuizManager.Teams.Count)
            {
                NextQuestion(QuizManager.Quiz.results[QuizManager.Quiz.results.IndexOf(QuizManager.CurrentQuestion) + 1]);
            }
        }

        public void NextQuestion(Question question)
        {
            QuizManager.CurrentQuestion = question;
            Clients.All.nextQuestion(question);
        }

        public void StartGame()
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
            if (t != null)
            {
                if (t.Players == null)
                    t.Players = new List<Player>();
                t.Players.Add(new Player
                {
                    Name = player,
                    ConnectionId = Context.ConnectionId
                });


                Groups.Add(Context.ConnectionId, team);
                var players = t.Players;
                Thread.Sleep(300);
                Clients.Group(team).addPlayerToTeam(players);
            }
        }

        public void AddPlayerNameToTeam(string team, string player)
        {
            Team t = QuizManager.Teams.FirstOrDefault(x => x.Name == team);
            if (t != null)
            {
                if (t.Players == null)
                    t.Players = new List<Player>();
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

        public void UpdateTeams(Team t)
        {
            Clients.Group(t.Name).addPlayerToTeam(t.Players);
        }
    }
}