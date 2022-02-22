using Microsoft.AspNetCore.SignalR;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.Server.Infrastructer.Hubs
{
    public class RFDHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public async Task SendPhishing(string flag, Phishing phishing)
        {
            await Clients.All.SendAsync("ReceivePhishing", flag, phishing);
        }


        public async Task SendInsider(string flag, Insider insider)
        {
            await Clients.All.SendAsync("ReceiveInsider", flag, insider);
        }

    }
}
