using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;

namespace World.WormTurns
{
    public class SkipTurn : IWormTurn
    {
        private static SkipTurn turnInstance = new SkipTurn();

        public static SkipTurn GetInstance()
        {
            return turnInstance;
        }

        public void Perform(int wormX, int wormY, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food)
        {
            
        }
    }
}
