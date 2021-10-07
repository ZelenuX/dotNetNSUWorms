using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurns;

namespace World.WormTurnPerformers
{
    public class WormDuplicatePerformer : IWormTurnPerformer<WormDuplicate>
    {
        public void ProcessTurn(int parentX, int parentY, WormDuplicate turn, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            if (worms.TryGet(parentX, parentY, out var parent))
            {
                int initHealth = WorldProperties.INIT_WORM_HEALTH;
                if (parent.Health <= initHealth)
                {
                    return;
                }
                int newX = parentX, newY = parentY;
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
                if (!food.TryGet(newX, newY, out var foodInCell))
                {
                    if (worms.TrySet(newX, newY, new Worm(parent.NameGenerator, initHealth, parent.WormStrategyProvider)))
                    {
                        parent.Health -= initHealth;
                    }
                }
            }
        }
    }
}
