using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;

namespace dotNetWormsTest.unitTests.services
{
    public class ReportWriterServiceTests
    {
        [Fact]
        public void FileWritingTest()
        {
            var world = new NewWorld();
            var writerService = new ReportWriterService("test-out.txt");

            writerService.WriteWorldState(world, 0);
            writerService.WriteWorldState(world, 3);
            writerService.Close();

            Assert.Equal("0\n1\n", File.ReadAllText("test-out.txt").Replace("\r\n", "\n"));
        }

        private class NewWorld : World.World
        {
            private int i = 0;
            public NewWorld() : base(null, null, null) { }
            public override string ToString()
            {
                return i++.ToString();
            }
        }
    }
}
