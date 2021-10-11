using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace Services
{
    public class WorldService : BackgroundService
    {
        private World.World world;
        private ReportWriterService reportWriter;
        private int NumberOfTurns;

        public WorldService(World.World world, ReportWriterService reportWriter, int numberOfTurns = 20)
        {
            this.world = world;
            this.reportWriter = reportWriter;
            NumberOfTurns = numberOfTurns;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            world.TryAddWorm(0, 0);
            reportWriter.WriteWorldState(world, 0);
            for (int i = 1; i <= NumberOfTurns; ++i)
            {
                world.nextTurn();
                reportWriter.WriteWorldState(world, i);
            }
            return Task.CompletedTask;
        }
    }
}
