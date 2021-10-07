using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using Utils.Generators;

namespace dotNetWormsTest.testUtils
{
    public class ControllableCoordsGenerator : ICoordsGenerator
    {
        public Coords nextCoords = new Coords(0, 0);
        public Coords NextCoords()
        {
            return nextCoords;
        }
    }
}
