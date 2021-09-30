using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
    class ReportWriterService
    {
        private StreamWriter Writer;
        public ReportWriterService(string reportPath)
        {
            Writer = File.CreateText(reportPath);
        }
        public void WriteWorldState(World.World world, int stateIndex)
        {
            string worldState = world.ToString();
            Console.WriteLine(stateIndex + ") " + worldState);
            Writer.WriteLine(worldState);
        }
    }
}
