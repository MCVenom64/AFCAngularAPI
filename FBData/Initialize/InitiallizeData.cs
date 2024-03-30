using FBData.Context;
using FBData.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBData.Initialize
{
    public static class InitializeData
    {
        private static void Initialize(FBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;
            }

            var players = new Player[]
            {
                new Player { PlayerName = "Thomas David", GoalScored = 0, Position = Position.GoalKeeper },
                new Player { PlayerName = "Thomas David", GoalScored = 0, Position = Position.GoalKeeper },
                new Player { PlayerName = "Thomas David", GoalScored = 0, Position = Position.GoalKeeper },
                new Player { PlayerName = "Thomas David", GoalScored = 0, Position = Position.Defender }

            };

            context.Players.AddRange(players);

            context.SaveChanges();


        }

        public static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<FBContext>();
                    InitializeData.Initialize(context);
                 
            }
        }

    }
}
