using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;
using Utils.Containers;

namespace World.WormTurnPerformers
{
    public class WormMovePerformer : IWormTurnPerformer<WormMove>
    {
        public void ProcessTurn(int wormX, int wormY, WormMove turn, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            int newX = wormX, newY = wormY;
            switch (turn.Direction)
            {
                case Direction.Up:
                    ++newY;
                    break;
                case Direction.Right:
                    ++newX;
                    break;
                case Direction.Down:
                    --newY;
                    break;
                case Direction.Left:
                    --newX;
                    break;
            }
            if (worms.TryGet(wormX, wormY, out var worm))
            {
                if (worms.TrySet(newX, newY, worm))
                {
                    worms.Remove(wormX, wormY);
                }
            }
        }
    }
}
