using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurns;

namespace World.WormTurnPerformers
{
    class WormDuplicatePerformer : IWormTurnPerformer<WormDuplicate>
    {
        public void ProcessTurn(int wormX, int wormY, WormDuplicate turn, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            if (worms.TryGet(wormX, wormY, out var worm))
            {
                int initHealth = WorldProperties.INIT_WORM_HEALTH;
                if (worm.Health <= initHealth)
                {
                    return;
                }
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
                if (!food.TryGet(newX, newY, out var foodInCell))
                {
                    if (worms.TrySet(newX, newY, new Worm(worm.NameGenerator, initHealth, worm.WormStrategyProvider)))
                    {
                        worm.Health -= initHealth;
                    }
                }
            }
        }
    }
}
