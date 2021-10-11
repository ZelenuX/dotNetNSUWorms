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
    public class WormMovePerformerTests
    {
        private WormMovePerformer performer;
        private Storage2dInfinite<Worm> worms;
        private Storage2dInfinite<Food> food;
        private Func<Worm> createWorm;
        public WormMovePerformerTests()
        {
            performer = new WormMovePerformer();
            worms = new Storage2dInfinite<Worm>();
            food = new Storage2dInfinite<Food>();
            var namesGenerator = new UniqueNamesGenerator("BobTheWorm_");
            createWorm = () => new Worm(namesGenerator, 10, new WormStrategyProviderService());
        }

        [Fact]
        public void MoveToEmptyCellTest()
        {
            worms.TrySet(0, 0, createWorm());
            worms.TrySet(10, 0, createWorm());
            worms.TrySet(20, 0, createWorm());
            worms.TrySet(30, 0, createWorm());

            performer.ProcessTurn(0, 0, new WormMove(Direction.Up), worms, food);
            performer.ProcessTurn(10, 0, new WormMove(Direction.Right), worms, food);
            performer.ProcessTurn(20, 0, new WormMove(Direction.Down), worms, food);
            performer.ProcessTurn(30, 0, new WormMove(Direction.Left), worms, food);

            Assert.False(worms.TryGet(0, 0, out _));
            Assert.True(worms.TryGet(0, 1, out _));
            Assert.False(worms.TryGet(10, 0, out _));
            Assert.True(worms.TryGet(11, 0, out _));
            Assert.False(worms.TryGet(20, 0, out _));
            Assert.True(worms.TryGet(20, -1, out _));
            Assert.False(worms.TryGet(30, 0, out _));
            Assert.True(worms.TryGet(29, 0, out _));
        }

        [Fact]
        public void MoveToCellWithWormTest()
        {
            var worm00 = createWorm();
            var worm01 = createWorm();
            worms.TrySet(0, 0, worm00);
            worms.TrySet(0, 1, worm01);

            performer.ProcessTurn(0, 0, new WormMove(Direction.Up), worms, food);

            Assert.True(worms.TryGet(0, 0, out var worm00after));
            Assert.True(worms.TryGet(0, 1, out var worm01after));
            Assert.Same(worm00after, worm00);
            Assert.Same(worm01after, worm01);
        }

        [Fact]
        public void MoveToCellWithFoodTest()
        {
            var worm00 = createWorm();
            worms.TrySet(0, 0, worm00);
            food.TrySet(0, 1, new Food(10));

            performer.ProcessTurn(0, 0, new WormMove(Direction.Up), worms, food);

            Assert.False(worms.TryGet(0, 0, out _));
            Assert.True(worms.TryGet(0, 1, out var worm01after));
            Assert.Same(worm01after, worm00);
        }
    }
}
