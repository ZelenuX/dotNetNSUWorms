using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;
using Utils;

namespace World.WormStrategies
{
    class MoveToNearestFoodStrategy : IWormStrategy
    {
        private static int calcDst(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
        }

        private Coords findNearestFoodRelative(WormAndWorldData wormAndWorldData)
        {
            int wormX = wormAndWorldData.WormX;
            int wormY = wormAndWorldData.WormY;
            int minX = wormX;
            int minY = wormY;
            int minDst = 0;
            wormAndWorldData.Food.ForEach((x, y, food) =>
            {
                int curDst = calcDst(wormX, wormY, x, y);
                if (curDst > 0 && (curDst < minDst || minDst == 0))
                {
                    minDst = curDst;
                    minX = x;
                    minY = y;
                }
            });
            return new Coords(minX - wormX, minY - wormY);
        }

        public IWormTurn GetNextTurn(WormAndWorldData wormAndWorldData)
        {
            Coords relFoodCoords = findNearestFoodRelative(wormAndWorldData);
            int x = relFoodCoords.X;
            int y = relFoodCoords.Y;
            Direction direction;
            if (x > 0)
            {
                direction = Direction.R;
            }
            else if (x < 0)
            {
                direction = Direction.L;
            }
            else
            {
                if (y > 0)
                {
                    direction = Direction.U;
                }
                else if (y < 0)
                {
                    direction = Direction.D;
                }
                else
                {
                    return SkipTurn.GetInstance();
                }
            }
            return WormMove.GetInstance(direction);
        }
    }
}
