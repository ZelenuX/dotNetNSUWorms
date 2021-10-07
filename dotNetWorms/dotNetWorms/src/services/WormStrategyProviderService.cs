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
    public class WormStrategyProviderService : BackgroundService
    {
        private Func<IWormStrategy> WormStrategyGenerator;

        public WormStrategyProviderService() {
            WormStrategyGenerator = () => new MoveToNearestFoodStrategy();
        }

        public WormStrategyProviderService(Func<IWormStrategy> wormStrategyGenerator)
        {
            WormStrategyGenerator = wormStrategyGenerator;
        }

        public IWormStrategy GetStrategy()
        {
            return WormStrategyGenerator.Invoke();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
