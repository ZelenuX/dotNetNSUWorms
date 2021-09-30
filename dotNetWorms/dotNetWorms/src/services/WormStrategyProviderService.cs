using System;
using System.Collections.Generic;
using System.Text;
using World;
using World.WormTurns;
using World.WormStrategies;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace Services
{
    class WormStrategyProviderService : BackgroundService
    {
        public IWormStrategy GetStrategy()
        {
            return new MoveToNearestFoodStrategy();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
