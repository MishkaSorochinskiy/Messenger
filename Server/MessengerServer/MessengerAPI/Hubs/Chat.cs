using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI.Hubs
{
    public class Chat:Hub
    {
        public async Task SendToAll(string name, string message)
        {
            await Clients.All.SendAsync("update", name, message);
        }
    }
}
