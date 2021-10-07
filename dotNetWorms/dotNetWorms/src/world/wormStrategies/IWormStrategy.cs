using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;

namespace World.WormStrategies
{
    public interface IWormStrategy
    {
        IWormTurn GetNextTurn(WormAndWorldData wormAndWorldData);
    }
}
