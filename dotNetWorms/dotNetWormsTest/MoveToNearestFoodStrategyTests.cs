using System;
using System.Collections.Generic;
using Xunit;
using dotNetWormsTest.testUtils;
using Services;
using Utils.Generators;
using Utils;
using World;

namespace dotNetWormsTest
{
    public class MoveToNearestFoodStrategyTests
    {
        private World.World world;
        private ControllableCoordsGenerator coordsGenerator;

        public MoveToNearestFoodStrategyTests()
        {
            coordsGenerator = new ControllableCoordsGenerator();
            world = new World.World(
                coordsGenerator,
                new UniqueNamesGenerator("BobTheWorm_"),
                new WormStrategyProviderService());
        }

        [Fact]
        public void NotEnoughFoodNotCrashes()
        {
            world.TryAddWorm(0, 0);
            coordsGenerator.nextCoords = new Coords(0, 10);

            world.TryAddWorm(0, 10);
            world.TryAddWorm(0, 11);
            world.TryAddWorm(1, 10);
            world.TryAddWorm(0, 9);
            world.TryAddWorm(-1, 10);
            world.nextTurn();

            world.TryAddWorm(0, 10);
            world.TryAddWorm(0, 11);
            world.TryAddWorm(1, 10);
            world.TryAddWorm(0, 9);
            world.TryAddWorm(-1, 10);
            world.nextTurn();
        }

        [Fact]
        public void MovingToNearestFood()
        {
            world.TryAddWorm(0, 0);
            coordsGenerator.nextCoords = new Coords(3, 4);

            for (int i = 3 + 4 - 1; i > 0; --i)
            {
                world.nextTurn();
                Assert.Equal(i, CalcNearestFoodDistance());
                coordsGenerator.nextCoords = new Coords(3, 4 + 1000 + i - 1);
            }
            world.nextTurn();
            Assert.Equal(1000, CalcNearestFoodDistance());
        }

        private int CalcNearestFoodDistance()
        {
            Coords wormCoords = GetWormCoords();
            int dst = int.MaxValue;
            world.GetFood().ForEach((coords, _) =>
            {
                int curDst = Math.Abs(coords.X - wormCoords.X)
                            + Math.Abs(coords.Y - wormCoords.Y);
                dst = curDst < dst ? curDst : dst;
            });
            return dst;
        }

        private Coords GetWormCoords()
        {
            Coords wormCoords = new Coords(0, 0) ;
            int wormsCount = 0;
            world.GetWorms().ForEach((coords, _) => {
                wormCoords = coords;
                ++wormsCount;
            });
            if (wormsCount != 1)
            {
                throw new Exception("worms on field - " + wormsCount + " (must be 1)");
            }
            return wormCoords;
        }
    }
}
