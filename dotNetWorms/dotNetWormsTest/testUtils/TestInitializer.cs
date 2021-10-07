using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.Generators;

namespace dotNetWormsTest.testUtils
{
    public class TestInitializer
    {
        protected ControllableCoordsGenerator coordsGenerator;
        protected World.World world;
        protected List<ControllableWormStrategy> strategies;

        public TestInitializer()
        {
            coordsGenerator = new ControllableCoordsGenerator();
            strategies = new List<ControllableWormStrategy>();
            world = new World.World(
                coordsGenerator,
                new UniqueNamesGenerator("BobTheWorm_"),
                new WormStrategyProviderService(() => new ControllableWormStrategy(strategies)));
        }
    }
}
