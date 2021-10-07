using System;
using System.Collections.Generic;
using System.Text;
using World.WormStrategies;

namespace World
{
    public class WorldProperties
    {
        public static int INIT_WORM_HEALTH { get; } = 10;
        public static int INIT_FOOD_FRESHNESS { get; } = 10;
        public static int FOOD_HEALING_EFFECT { get; } = 10;
        public static IWormStrategy GetWormStrategy()
        {
            return new MoveToNearestFoodStrategy();
        }
    }
}
