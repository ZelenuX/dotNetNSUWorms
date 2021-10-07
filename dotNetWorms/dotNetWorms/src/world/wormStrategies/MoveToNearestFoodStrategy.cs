using System;
using System.Collections.Generic;
using System.Text;
using World.WormTurns;
using Utils;

namespace World.WormStrategies
{
    public class MoveToNearestFoodStrategy : IWormStrategy
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
                direction = Direction.Right;
            }
            else if (x < 0)
            {
                direction = Direction.Left;
            }
            else
            {
                if (y > 0)
                {
                    direction = Direction.Up;
                }
                else if (y < 0)
                {
                    direction = Direction.Down;
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
