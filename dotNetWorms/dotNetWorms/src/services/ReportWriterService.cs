using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
    public class ReportWriterService
    {
        private StreamWriter Writer;
        public ReportWriterService(string reportPath)
        {
            if (reportPath != null)
            {
                Writer = File.CreateText(reportPath);
            }
        }
        public virtual void WriteWorldState(World.World world, int stateIndex)
        {
            string worldState = world.ToString();
            Console.WriteLine(stateIndex + ") " + worldState);
            Writer.WriteLine(worldState);
        }
        public virtual void Close()
        {
            Writer.Close();
        }
    }
}
