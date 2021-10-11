using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dotNetWormsTest.unitTests.services
{
    public class WorldServiceTests
    {
        [Fact]
        public void WorldCycleTest()
        {
            var world = new NewWorld();
            var worldService = new NewWorldService(world, new NewWriterService(), 5);

            worldService.ExecuteAsync();

            Assert.Equal("a0,0nnnnn", world.History);
        }

        private class NewWorldService : WorldService
        {
            public NewWorldService(World.World world, ReportWriterService reportWriter, int numberOfTurns = 20)
                : base(world, reportWriter, numberOfTurns) { }
            public void ExecuteAsync()
            {
                base.ExecuteAsync(System.Threading.CancellationToken.None);
            }
        }

        private class NewWorld : World.World
        {
            public string History { get; private set; } = "";
            public NewWorld() : base(null, null, null) { }
            public override bool TryAddWorm(int x, int y)
            {
                History += "a" + x + "," + y;
                return true;
            }
            public override void nextTurn()
            {
                History += "n";
            }
        }

        private class NewWriterService : ReportWriterService
        {
            public NewWriterService() : base(null) { }
            public override void WriteWorldState(World.World world, int stateIndex){ }
            public override void Close() { }
        }
    }
}
