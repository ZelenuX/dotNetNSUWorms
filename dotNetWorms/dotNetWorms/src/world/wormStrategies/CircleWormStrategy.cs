using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;

namespace World.WormStrategies
{
    public class CircleWormStrategy : IWormStrategy
    {
        private Direction firstDirection = Direction.Up;
        private Direction[] directions = { Direction.Right,
            Direction.Down, Direction.Down,
            Direction.Left, Direction.Left,
            Direction.Up, Direction.Up,
            Direction.Right };
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
