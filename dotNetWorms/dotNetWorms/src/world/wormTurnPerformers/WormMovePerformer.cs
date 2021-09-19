using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;
using Utils.Containers;

namespace World.WormTurnPerformers
{
    class WormMovePerformer : IWormTurnPerformer<WormMove>
    {
        public void ProcessTurn(int wormX, int wormY, WormMove turn, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            int newX = wormX, newY = wormY;
            switch (turn.Direction)
            {
                case Direction.U:
                    ++newY;
                    break;
                case Direction.R:
                    ++newX;
                    break;
                case Direction.D:
                    --newY;
                    break;
                case Direction.L:
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
