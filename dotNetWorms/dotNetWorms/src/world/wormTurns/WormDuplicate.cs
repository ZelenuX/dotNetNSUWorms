using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurnPerformers;

namespace World.WormTurns
{
    public class WormDuplicate : IWormTurn
    {
        public static WormDuplicatePerformer turnPerformer = new WormDuplicatePerformer();
        private static Dictionary<Direction, WormDuplicate> cashed = new Dictionary<Direction, WormDuplicate>();
        public static WormDuplicate GetInstance(Direction direction)
        {
            if (cashed.TryGetValue(direction, out var value))
            {
                return value;
            }
            else
            {
                var newTurn = new WormDuplicate(direction);
                cashed.TryAdd(direction, newTurn);
                return newTurn;
            }
        }

        public void Perform(int wormX, int wormY, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            turnPerformer.ProcessTurn(wormX, wormY, this, worms, food);
        }

        public Direction Direction { get; }
        public WormDuplicate(Direction direction)
        {
            this.Direction = direction;
        }
    }
}
