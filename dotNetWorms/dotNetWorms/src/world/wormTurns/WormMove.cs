using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurnPerformers;

namespace World.WormTurns
{
    class WormMove : IWormTurn
    {
        private static Dictionary<Direction, WormMove> cashed = new Dictionary<Direction, WormMove>();
        public static WormMove GetInstance(Direction direction)
        {
            if (cashed.TryGetValue(direction, out var value))
            {
                return value;
            }
            else
            {
                var newTurn = new WormMove(direction);
                cashed.TryAdd(direction, newTurn);
                return newTurn;
            }
        }
        public static WormMovePerformer turnPerformer = new WormMovePerformer();

        public void Perform(int wormX, int wormY, IStorage2d<Worm> worms)
        {
            turnPerformer.ProcessTurn(wormX, wormY, this, worms);
        }

        public Direction Direction { get; }
        public WormMove(Direction direction)
        {
            this.Direction = direction;
        }
    }
}
