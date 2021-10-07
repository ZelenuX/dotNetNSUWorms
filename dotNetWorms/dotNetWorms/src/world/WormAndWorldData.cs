using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;

namespace World
{
    public struct WormAndWorldData
    {
        public int WormX;
        public int WormY;
        public StorageDataProvider<Worm> Worms;
        public StorageDataProvider<Food> Food;
        public WormAndWorldData(int wormX, int wormY, StorageDataProvider<Worm> worms, StorageDataProvider<Food> food)
        {
            this.WormX = wormX;
            this.WormY = wormY;
            this.Worms = worms;
            this.Food = food;
        }
    }
}
