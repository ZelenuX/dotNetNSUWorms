using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using World.WormTurns;

namespace World
{
    class World
    {
        private IStorage2d<Worm> wormsOnField = new Storage2dInfinite<Worm>();//todo double buffered storage
        private StringBuilder stringBuilder = new StringBuilder();

        public bool TryAddWorm(int x, int y)
        {
            return wormsOnField.TrySet(x, y, new Worm());
        }

        public void nextTurn()
        {
            wormsOnField.ForEach((x, y, worm) => worm.GetNextTurn().Perform(x, y, wormsOnField));
        }

        override public string ToString()
        {
            stringBuilder.Clear();
            stringBuilder.Append("Worms:[");
            if (wormsOnField.Count > 0)
            {
                wormsOnField.ForEach((x, y, worm)
                    => stringBuilder.Append(worm.name).Append("(").Append(x).Append(",").Append(y).Append("),"));
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}
