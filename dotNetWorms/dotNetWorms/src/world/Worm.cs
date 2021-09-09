using System;
using System.Collections.Generic;
using System.Text;
using Utils.Generators;
using World.WormTurns;

namespace World
{
    class Worm
    {
        private static UniqueNamesGenerator nameGenerator = new UniqueNamesGenerator("BobTheWorm_");

        public string name { get; }
        public int food { get; }
        public IWormStrategy wormStrategy { set; private get; } = new CircleWormStrategy();

        public Worm(int food = 10)
        {
            this.food = food;
            name = nameGenerator.generate();
        }

        public IWormTurn GetNextTurn()
        {
            return wormStrategy.GetNextTurn();
        }
    }
}
