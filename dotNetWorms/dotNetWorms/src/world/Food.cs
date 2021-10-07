using System;
using System.Collections.Generic;
using System.Text;

namespace World
{
    public class Food
    {
        private int freshness;

        public Food(int freshness)
        {
            this.freshness = freshness;
        }
        public void DecFreshness()
        {
            --freshness;
        }
        public bool IsFresh()
        {
            return freshness > 0;
        }
    }
}
