using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;
using Utils.Containers;

namespace World.WormTurnPerformers
{
    interface IWormTurnPerformer<TurnType> where TurnType : IWormTurn
    {
        void ProcessTurn(int wormX, int wormY, TurnType turn, IStorage2d<Worm> worms);
    }
}
