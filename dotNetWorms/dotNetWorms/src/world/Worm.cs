using System;
using System.Collections.Generic;
using System.Text;
using Utils.Generators;
using World.WormTurns;
using World.WormStrategies;
using Services;

namespace World
{
    class Worm
    {
        public string Name { get; }
        public int Health { get; set; }
        public IWormStrategy WormStrategy { set; private get; }
        public UniqueNamesGenerator NameGenerator { get; }
        public WormStrategyProviderService WormStrategyProvider { get; }

        public Worm(UniqueNamesGenerator nameGenerator, int health, WormStrategyProviderService wormStrategyProvider)
        {
            Health = health;
            Name = nameGenerator.generate();
            WormStrategy = wormStrategyProvider.GetStrategy();
            WormStrategyProvider = wormStrategyProvider;
            NameGenerator = nameGenerator;
        }

        public IWormTurn GetNextTurn(WormAndWorldData data)
        {
            return WormStrategy.GetNextTurn(data);
        }
    }
}
