using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlazorRPG.Server.Hubs
{
    public class DataHub:Hub
    {
        public async Task SyncRecord()
        {
            await Clients.Others.SendAsync("ReceiveMessage");

        }

    }
}
