using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Utils.Generators;

namespace dotNetWormsTest.unitTests.utils
{
    public class NormalCoordsGeneratorTests
    {
        [Fact]
        public void MeanTest()
        {
            int iterations = 10000;
            var generator = new NormalCoordsGenerator(5, 7);
            int sumX = 0;
            int sumY = 0;

            for (int i = 0; i < iterations; ++i)
            {
                var coords = generator.NextCoords();
                sumX += coords.X;
                sumY += coords.Y;
            }
            double meanX = (double)sumX / iterations;
            double meanY = (double)sumY / iterations;

            Assert.InRange(meanX, 4.9, 5.1);
            Assert.InRange(meanY, 4.9, 5.1);
        }
        
        [Fact]
        public void DevTest()
        {
            int iterations = 10000;
            var generator = new NormalCoordsGenerator(5, 7);
            int sumDifX = 0;
            int sumDifY = 0;

            for (int i = 0; i < iterations; ++i)
            {
                var coords = generator.NextCoords();
                sumDifX += (coords.X - 5) * (coords.X - 5);
                sumDifY += (coords.Y - 5) * (coords.Y - 5);
            }
            double devX = Math.Sqrt((double)sumDifX / iterations);
            double devY = Math.Sqrt((double)sumDifY / iterations);

            Assert.InRange(devX, 6.9, 7.1);
            Assert.InRange(devY, 6.9, 7.1);
        }
    }
}
