using System;
using System.Collections.Generic;
using System.Text;
using Utils.Generators;
using World.WormTurns;
using World.WormStrategies;

namespace World
{
    class Worm
    {
        private static UniqueNamesGenerator nameGenerator = new UniqueNamesGenerator("BobTheWorm_");

        public string Name { get; }
        public int Health { get; set; }
        public IWormStrategy wormStrategy { set; private get; } = WorldProperties.GetWormStrategy();

        public Worm(int health)
        {
            this.Health = health;
            Name = nameGenerator.generate();
        }

        public IWormTurn GetNextTurn(WormAndWorldData data)
        {
            return wormStrategy.GetNextTurn(data);
        }
    }
}
