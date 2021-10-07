using System;
using System.Collections.Generic;
using System.Text;
using World;
using World.WormStrategies;
using World.WormTurns;

namespace dotNetWormsTest.testUtils
{
    public class ControllableWormStrategy : IWormStrategy
    {
        public ControllableWormStrategy(List<ControllableWormStrategy> instances)
        {
            instances.Add(this);
        }
        public IWormTurn NextTurn = new SkipTurn();
        public IWormTurn GetNextTurn(WormAndWorldData wormAndWorldData)
        {
            return NextTurn;
        }
    }
}
