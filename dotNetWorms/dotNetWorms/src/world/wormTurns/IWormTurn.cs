using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurnPerformers;

namespace World.WormTurns
{
    interface IWormTurn {
        void Perform(int wormX, int wormY, IStorage2d<Worm> worms);
    }

    enum Direction
    {
        U, R, D, L
    }    
}
