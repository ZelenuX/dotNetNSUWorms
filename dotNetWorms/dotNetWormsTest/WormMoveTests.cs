using System;
using Xunit;
using World;
using Utils.Generators;
using Services;
using World.WormTurns;
using System.Collections.Generic;
using Utils;
using dotNetWormsTest.testUtils;

namespace dotNetWormsTest
{
    public class WormMoveTests : TestInitializer
    {
        [Fact]
        public void MoveOnEmptyTest()
        {
            world.TryAddWorm(0, 0);
            var wormStrategy = strategies.ToArray()[0];
            coordsGenerator.nextCoords = new Coords(10, 10);
            wormStrategy.NextTurn = new WormMove(Direction.Up);

            Assert.True(world.GetWorms().TryGet(0, 0, out _));
            world.nextTurn();
            Assert.False(world.GetWorms().TryGet(0, 0, out _));
            Assert.True(world.GetWorms().TryGet(0, 1, out _));
        }

        [Fact]
        public void MoveOnFoodTest()
        {
            world.TryAddWorm(0, 0);
            var wormStrategy = strategies.ToArray()[0];
            coordsGenerator.nextCoords = new Coords(0, 1);
            wormStrategy.NextTurn = new WormMove(Direction.Up);
            Worm worm;
            world.GetWorms().TryGet(0, 0, out worm);

            Assert.Equal(10, worm.Health);
            world.nextTurn();
            Assert.Equal(19, worm.Health);
        }

        [Fact]
        public void MoveOnCellWithWormTest()
        {
            world.TryAddWorm(0, 0);
            world.TryAddWorm(0, 1);
            var wormStrategy0 = strategies.ToArray()[0];
            var wormStrategy1 = strategies.ToArray()[1];
            coordsGenerator.nextCoords = new Coords(0, 1);
            wormStrategy0.NextTurn = new WormMove(Direction.Up);
            wormStrategy1.NextTurn = new SkipTurn();
            Worm worm0;
            Worm worm1;
            world.GetWorms().TryGet(0, 0, out worm0);
            world.GetWorms().TryGet(0, 1, out worm1);

            world.nextTurn();
            Worm worm0after;
            Worm worm1after;
            Assert.True(world.GetWorms().TryGet(0, 0, out worm0after));
            Assert.True(world.GetWorms().TryGet(0, 1, out worm1after));
            Assert.Same(worm0, worm0after);
            Assert.Same(worm1, worm1after);
        }
    }
}
