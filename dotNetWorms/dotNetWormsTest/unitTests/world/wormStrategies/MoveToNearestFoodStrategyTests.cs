using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using Utils.Containers;
using World;
using World.WormStrategies;
using Utils.Containers;
using Xunit;
using World.WormTurns;

namespace dotNetWormsTest.unitTests.world.wormStrategies
{
    public class MoveToNearestFoodStrategyTests
    {
        [Fact]
        public void MovingToNearestFoodTest()
        {
            var strategy = new MoveToNearestFoodStrategy();
            var worms = new Storage2dInfinite<Worm>();
            var foods = new Storage2dInfinite<Food>();
            var wwData = new WormAndWorldData(1, 2, worms, foods);
            foods.TrySet(1 + 2, 2 + 5, new Food(4));
            foods.TrySet(1 - 3, 2 - 5, new Food(100));

            var nextTurn = strategy.GetNextTurn(wwData);

            Assert.IsType<WormMove>(nextTurn);
            Assert.True(((WormMove)nextTurn).Direction == Direction.Right || ((WormMove)nextTurn).Direction == Direction.Up);
        }
    }
}
