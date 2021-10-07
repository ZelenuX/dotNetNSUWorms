using System;
using Xunit;
using dotNetWormsTest.testUtils;
using World.WormTurns;
using Utils;
using World;

namespace dotNetWormsTest
{
    public class WormDuplicationTests : TestInitializer
    {
        [Fact]
        public void DuplicateToEmpty()
        {
            coordsGenerator.nextCoords = new Coords(10, 10);
            Worm parent = AddFirstWorm();
            parent.Health = 20;

            world.nextTurn();

            Assert.True(world.GetWorms().TryGet(0, 0, out var parentAfter));
            Assert.True(world.GetWorms().TryGet(0, 1, out var child));
            Assert.Same(parent, parentAfter);
            Assert.Equal(9, parentAfter.Health);
            Assert.Equal(10, child.Health);
        }
        [Fact]
        public void DuplicateToCellWithWorm()
        {
            coordsGenerator.nextCoords = new Coords(10, 10);
            Worm parent = AddFirstWorm();
            parent.Health = 20;
            world.TryAddWorm(0, 1);
            world.GetWorms().TryGet(0, 1, out var another);

            world.nextTurn();

            Assert.True(world.GetWorms().TryGet(0, 0, out var parentAfter));
            Assert.True(world.GetWorms().TryGet(0, 1, out var notChild));
            Assert.Same(parent, parentAfter);
            Assert.Same(another, notChild);
            Assert.Equal(19, parentAfter.Health);
            Assert.Equal(9, another.Health);
        }
        [Fact]
        public void DuplicateToCellWithFood()
        {
            coordsGenerator.nextCoords = new Coords(0, 1);
            Worm parent = AddFirstWorm();
            parent.Health = 20;

            world.nextTurn();

            Assert.True(world.GetWorms().TryGet(0, 0, out var parentAfter));
            Assert.False(world.GetWorms().TryGet(0, 1, out _));
            Assert.True(world.GetFood().TryGet(0, 1, out _));
            Assert.Same(parent, parentAfter);
            Assert.Equal(19, parentAfter.Health);
        }
        [Fact]
        public void DuplicateNotEnoughHealth()
        {
            coordsGenerator.nextCoords = new Coords(10, 10);
            Worm parent = AddFirstWorm();
            parent.Health = 10;

            world.nextTurn();

            Assert.True(world.GetWorms().TryGet(0, 0, out var parentAfter));
            Assert.False(world.GetWorms().TryGet(0, 1, out _));
            Assert.Same(parent, parentAfter);
            Assert.Equal(9, parentAfter.Health);
        }

        private Worm AddFirstWorm()
        {
            world.TryAddWorm(0, 0);
            var strategy = strategies.ToArray()[0];
            strategy.NextTurn = new WormDuplicate(Direction.Up);
            world.GetWorms().TryGet(0, 0, out var parent);
            return parent;
        }
    }
}
