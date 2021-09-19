using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;

namespace World.WormStrategies
{
    class CircleWormStrategy : IWormStrategy
    {
        private Direction firstDirection = Direction.U;
        private Direction[] directions = { Direction.R,
            Direction.D, Direction.D,
            Direction.L, Direction.L,
            Direction.U, Direction.U,
            Direction.R };
        private int turnNumber = -1;

        public IWormTurn GetNextTurn(WormAndWorldData wormAndWorldData)
        {
            if (turnNumber < 0)
            {
                turnNumber = 0;
                return WormMove.GetInstance(firstDirection);
            }
            else
            {
                return WormMove.GetInstance(directions[turnNumber++ % directions.Length]);
            }
        }
    }
}
