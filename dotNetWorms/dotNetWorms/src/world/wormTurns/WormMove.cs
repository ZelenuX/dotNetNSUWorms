using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurnPerformers;

namespace World.WormTurns
{
    public class WormMove : IWormTurn
    {
        public static WormMovePerformer turnPerformer = new WormMovePerformer();
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
        
        public void Perform(int wormX, int wormY, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            turnPerformer.ProcessTurn(wormX, wormY, this, worms, food);
        }

        public Direction Direction { get; }
        public WormMove(Direction direction)
        {
            this.Direction = direction;
        }
    }
}
