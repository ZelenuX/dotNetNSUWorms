using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace Services
{
    class WorldService : BackgroundService
    {
        private World.World world;
        private ReportWriterService reportWriter;

        public WorldService(World.World world, ReportWriterService reportWriter)
        {
            this.world = world;
            this.reportWriter = reportWriter;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            world.TryAddWorm(0, 0);
            reportWriter.WriteWorldState(world, 0);
            for (int i = 1; i <= 20; ++i)
            {
                world.nextTurn();
                reportWriter.WriteWorldState(world, i);
            }
            return Task.CompletedTask;
        }
    }
}
