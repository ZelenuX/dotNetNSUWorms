using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurnPerformers;

namespace World.WormTurns
{
    public interface IWormTurn {
        void Perform(int wormX, int wormY, AbstractStorage2d<Worm> worms, AbstractStorage2d<Food> food);
    }

    public enum Direction
    {
        Up, Right, Down, Left
    }    
}
