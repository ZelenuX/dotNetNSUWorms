using System;
using System.Collections.Generic;
using Xunit;
using dotNetWormsTest.testUtils;
using Utils.Generators;
using Services;
using Utils;
using World.WormTurns;

namespace dotNetWormsTest
{
    public class GeneratorsTests : TestInitializer
    {
        [Fact]
        public void NameUniquenessTest()
        {
            for (int i = 0; i < 10000; ++i)
            {
                world.TryAddWorm(0, i);
            }
            var names = new HashSet<String>();
            world.GetWorms().ForEach((coords, worm) => {
                Assert.True(names.Add(worm.Name));
            });
        }
        [Fact]
        public void FoodPositionUniquenessTest()
        {
            world = new World.World(
                new NormalCoordsGenerator(0, 5),
                new UniqueNamesGenerator("BobTheWorm_"),
                new WormStrategyProviderService());
            for (int i = 0; i < 10; ++i)
            {
                world.nextTurn();
            }
            var foodCoords = new HashSet<Coords>();
            for (int i = 0; i < 10000; ++i)
            {
                world.nextTurn();
                int foodNumber = 0;
                foodCoords.Clear();
                world.GetFood().ForEach((coords, food) => {
                    ++foodNumber;
                    Assert.True(foodCoords.Add(coords));
                });
                Assert.Equal(10, foodNumber);
            }
        }
        [Fact]
        void AddFoodInCellWithWormTest()
        {
            coordsGenerator.nextCoords = new Coords(0, 0);
            world.TryAddWorm(0, 0);
            strategies.ToArray()[0].NextTurn = new SkipTurn();
            world.GetWorms().TryGet(0, 0, out var worm);
            Assert.Equal(10, worm.Health);

            world.nextTurn();

            Assert.Equal(19, worm.Health);
            Assert.False(world.GetFood().TryGet(0, 0, out _));
        }
    }
}
