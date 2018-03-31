using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Web
{
    public class SeatBookingHub : Hub
    {
        private static Dictionary<string, string> _connectionIdsAndNames = new Dictionary<string, string>();

        private static ConcurrentBag<string> _animals = new ConcurrentBag<string>
        {
            "Antilope",
            "Dog",
            "Lion",
            "Zebra",
        };

        public override async Task OnConnectedAsync()
        {
            var connectionID = Context.ConnectionId;
            await base.OnConnectedAsync();

            string selectedAnimal;
            lock (_connectionIdsAndNames)
            {
                selectedAnimal = _animals.Except(_connectionIdsAndNames.Select(connections => connections.Value)).OrderBy(x => Guid.NewGuid()).First();
                _connectionIdsAndNames.Add(connectionID, selectedAnimal);
            }

            await Clients.Caller.SendAsync("animalAssigned", selectedAnimal);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectionIdsAndNames.Remove(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception); 
        }
    }
}