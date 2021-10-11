using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using World.WormTurnPerformers;
using Utils.Containers;
using World;
using World.WormTurns;
using Utils.Generators;
using Services;

namespace dotNetWormsTest.unitTests.world.wormTurnPerformers
{
    public class WormDuplicatePerformerTests
    {
        private WormDuplicatePerformer performer;
        private Storage2dInfinite<Worm> worms;
        private Storage2dInfinite<Food> food;
        private Func<int, Worm> createWorm;
        public WormDuplicatePerformerTests()
        {
            performer = new WormDuplicatePerformer();
            worms = new Storage2dInfinite<Worm>();
            food = new Storage2dInfinite<Food>();
            var namesGenerator = new UniqueNamesGenerator("BobTheWorm_");
            createWorm = (health) => new Worm(namesGenerator, health, new WormStrategyProviderService());
        }

        [Fact]
        public void DuplicateToEmptyCellTest()
        {
            worms.TrySet(0, 0, createWorm(16));
            worms.TrySet(10, 0, createWorm(16));
            worms.TrySet(20, 0, createWorm(16));
            worms.TrySet(30, 0, createWorm(16));

            performer.ProcessTurn(0, 0, new WormDuplicate(Direction.Up), worms, food);
            performer.ProcessTurn(10, 0, new WormDuplicate(Direction.Right), worms, food);
            performer.ProcessTurn(20, 0, new WormDuplicate(Direction.Down), worms, food);
            performer.ProcessTurn(30, 0, new WormDuplicate(Direction.Left), worms, food);

            Assert.True(worms.TryGet(0, 0, out var w0));
            Assert.True(worms.TryGet(0, 1, out var w0c));
            Assert.Equal(6, w0.Health);
            Assert.Equal(10, w0c.Health);
            Assert.True(worms.TryGet(10, 0, out var w1));
            Assert.True(worms.TryGet(11, 0, out var w1c));
            Assert.Equal(6, w1.Health);
            Assert.Equal(10, w1c.Health);
            Assert.True(worms.TryGet(20, 0, out var w2));
            Assert.True(worms.TryGet(20, -1, out var w2c));
            Assert.Equal(6, w2.Health);
            Assert.Equal(10, w2c.Health);
            Assert.True(worms.TryGet(30, 0, out var w3));
            Assert.True(worms.TryGet(29, 0, out var w3c));
            Assert.Equal(6, w3.Health);
            Assert.Equal(10, w3c.Health);

        }

        [Fact]
        public void MoveToCellWithWormTest()
        {
            var worm00 = createWorm(20);
            var worm01 = createWorm(20);
            worms.TrySet(0, 0, worm00);
            worms.TrySet(0, 1, worm01);

            performer.ProcessTurn(0, 0, new WormDuplicate(Direction.Up), worms, food);

            Assert.True(worms.TryGet(0, 0, out var worm00after));
            Assert.True(worms.TryGet(0, 1, out var worm01after));
            Assert.Same(worm00after, worm00);
            Assert.Same(worm01after, worm01);
            Assert.Equal(20, worm00.Health);
            Assert.Equal(20, worm01.Health);
        }

        [Fact]
        public void MoveToCellWithFoodTest()
        {
            var worm00 = createWorm(20);
            worms.TrySet(0, 0, worm00);
            food.TrySet(0, 1, new Food(10));

            performer.ProcessTurn(0, 0, new WormDuplicate(Direction.Up), worms, food);

            Assert.True(worms.TryGet(0, 0, out var worm00after));
            Assert.False(worms.TryGet(0, 1, out _));
            Assert.Same(worm00after, worm00);
            Assert.Equal(20, worm00.Health);
            Assert.True(food.TryGet(0, 1, out _));
        }
    }
}
