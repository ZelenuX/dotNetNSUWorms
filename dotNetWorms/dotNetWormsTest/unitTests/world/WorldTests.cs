using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using dotNetWormsTest.testUtils;
using Utils.Generators;
using World.WormTurns;
using Utils;

namespace dotNetWormsTest.unitTests.world
{
    public class WorldTests
    {
        private World.World World;
        private ControllableCoordsGenerator FoodCoordsGenerator;
        private List<ControllableWormStrategy> WormStrategies;
        public WorldTests()
        {
            FoodCoordsGenerator = new ControllableCoordsGenerator();
            WormStrategies = new List<ControllableWormStrategy>();
            World = new World.World(FoodCoordsGenerator,
                new UniqueNamesGenerator("BobTheWorm_"),
                new Services.WormStrategyProviderService(()=> new ControllableWormStrategy(WormStrategies)));
        }

        [Fact]
        public void WormEatingFoodTest()
        {
            World.TryAddWorm(0, 0);
            WormStrategies.ToArray()[0].NextTurn = new WormMove(Direction.Up);
            FoodCoordsGenerator.nextCoords = new Coords(0, 1);

            World.nextTurn();

            Assert.True(World.GetWorms().TryGet(0, 1, out var worm));
            Assert.Equal(19, worm.Health);
        }
        [Fact]
        public void AddFoodToCellWithWorm()
        {
            World.TryAddWorm(0, 0);
            WormStrategies.ToArray()[0].NextTurn = new WormMove(Direction.Up);
            FoodCoordsGenerator.nextCoords = new Coords(0, 0);

            World.nextTurn();

            Assert.True(World.GetWorms().TryGet(0, 1, out var worm));
            Assert.Equal(19, worm.Health);
        }
    }
}
