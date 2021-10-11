using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Utils.Containers;

namespace dotNetWormsTest.unitTests.utils
{
    public class Storage2dInfiniteTests
    {
        [Fact]
        public void SetGetTest()
        {
            var storage = new Storage2dInfinite<int>();

            bool s1 = storage.TrySet(0, 1, 10);
            bool s1_2 = storage.TrySet(0, 1, 11);
            bool s2 = storage.TrySet(0, 2, 20);
            bool g1 = storage.TryGet(0, 1, out var o1);
            bool g2 = storage.TryGet(0, 2, out var o2);
            bool g3 = storage.TryGet(0, 3, out var o3);

            Assert.True(s1);
            Assert.False(s1_2);
            Assert.True(s2);
            Assert.True(g1);
            Assert.Equal(10, o1);
            Assert.True(g2);
            Assert.Equal(20, o2);
            Assert.False(g3);
        }

        [Fact]
        public void RemoveTest()
        {
            var storage = new Storage2dInfinite<int>();
            storage.TrySet(0, 1, 10);

            bool r0 = storage.Remove(1, 0);
            bool r1 = storage.Remove(0, 1);
            bool g1 = storage.TryGet(0, 1, out _);
            bool r2 = storage.Remove(0, 1);

            Assert.False(r0);
            Assert.True(r1);
            Assert.False(g1);
            Assert.False(r2);
        }
    }
}
