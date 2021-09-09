using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;

namespace World
{
    interface IWormStrategy
    {
        IWormTurn GetNextTurn();
    }
}
