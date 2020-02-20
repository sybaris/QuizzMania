﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMania.Hubs
{
    public class QuizzHub : Hub
    {
        public async Task SendMessage(string user, int id, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, id, message);
        }

        public async Task DisabledButton()
        {
            await Clients.All.SendAsync("ButtonDisabled");
        }

        public async Task NextQuestion()
        {
            await Clients.All.SendAsync("ButtonNext");
        }
    }
}
