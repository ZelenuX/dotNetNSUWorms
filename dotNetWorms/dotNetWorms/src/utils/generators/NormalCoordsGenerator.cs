using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Generators
{
    class NormalCoordsGenerator
    {
        private Random rand = new Random();
        private double mean;
        private double dev;
        private int nextInt()
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();

            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return (int)Math.Round(mean + dev* randStdNormal); //random normal(mean,dev^2)
    }

        public NormalCoordsGenerator(double mean, double dev)
        {
            this.mean = mean;
            this.dev = dev;
        }
        public Coords NextCoords()
        {
            return new Coords(nextInt(), nextInt());
        }
    }
}
