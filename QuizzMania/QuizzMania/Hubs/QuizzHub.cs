using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMania.Hubs
{
    public class QuizzHub : Hub
    {
        public async Task SendUserAnswer(string user, int id, string reponse)
        {
            await Clients.All.SendAsync("ReceiveUserAnswer", user, id, reponse);
        }

        public async Task SendAdminDisplayAllAnswers()
        {
            await Clients.All.SendAsync("ReceiveAdminDisplayAllAnswers");
        }

        public async Task SendAdminNextQuestion()
        {
            await Clients.All.SendAsync("ReceiveAdminNextQuestion");
        }
    }
}
