using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace Web
{
    public class SeatBookingHub : Hub
    {
        private static Dictionary<string, string> _connectionIdsAndAssignedAnimal = new Dictionary<string, string>();
        private static ConcurrentBag<string> _animals = new ConcurrentBag<string>();

        public SeatBookingHub(IHostingEnvironment environment)
        {
            if (_animals == null || _animals.Count == 0)
            {
                string[] filesindirectory = Directory.GetFiles(Path.Combine(environment.WebRootPath, "images", "animal-icons"));
                _animals = new ConcurrentBag<string>(filesindirectory.Select(fullFilePath => Path.GetFileNameWithoutExtension(fullFilePath)));
            }
        }

        public override async Task OnConnectedAsync()
        {
            var connectionID = Context.ConnectionId;
            await base.OnConnectedAsync();

            string assignedAnimal;
            lock (_connectionIdsAndAssignedAnimal)
            {
                assignedAnimal = _animals.Except(_connectionIdsAndAssignedAnimal.Select(connections => connections.Value)).OrderBy(x => Guid.NewGuid()).First();
                _connectionIdsAndAssignedAnimal.Add(connectionID, assignedAnimal);
            }

            await Clients.Caller.SendAsync("animalAssigned", assignedAnimal);
            await Clients.Others.SendAsync("newAnimalInTheFlock", assignedAnimal);
            await Clients.Caller.SendAsync("updatedAnimalList", _connectionIdsAndAssignedAnimal.Select(c => c.Value));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var animalKilledOff = _connectionIdsAndAssignedAnimal[Context.ConnectionId];
            _connectionIdsAndAssignedAnimal.Remove(Context.ConnectionId);

            Clients.All.SendAsync("animalKilled", animalKilledOff);

            return base.OnDisconnectedAsync(exception); 
        }
    }
}