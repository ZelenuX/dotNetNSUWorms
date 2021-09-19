using System;
using System.IO;

namespace dotNetWorms
{
    class Program
    {
        private static void writeWorldState(World.World world, StreamWriter writer, int stateIndex)
        {
            string worldState = world.ToString();
            Console.WriteLine(stateIndex + ") " + worldState);
            writer.WriteLine(worldState);
        }

        static void Main(string[] args)
        {
            var fileWriter = File.CreateText("./out.txt");
            World.World world = new World.World();
            world.TryAddWorm(0, 0);
            Console.WriteLine("Worm coords:");
            writeWorldState(world, fileWriter, 0);
            for (int i = 1; i <= 20; ++i)
            {
                world.nextTurn();
                writeWorldState(world, fileWriter, i);
            }
            fileWriter.Close();
            Console.Read();
        }
    }
}
