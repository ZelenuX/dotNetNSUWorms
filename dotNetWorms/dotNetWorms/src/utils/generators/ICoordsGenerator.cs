using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Generators
{
    public interface ICoordsGenerator
    {
        Coords NextCoords();
    }
}
